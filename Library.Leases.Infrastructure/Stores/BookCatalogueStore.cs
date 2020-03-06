using System.Collections.Generic;
using System.Linq;
using Library.Leases.Domain.Models;
using Library.Leases.Domain.Stores;
using ItemModuleItemStore = Library.Items.Infrastructure.Stores.ItemStore;
using ItemModuleItem = Library.Items.Infrastructure.Data.Item;

namespace Library.Leases.Infrastructure.Stores
{
    public class BookCatalogueStore : IBookCatalogueStore
    {
        private readonly ItemModuleItemStore _itemStore;

        public BookCatalogueStore(ItemModuleItemStore itemStore)
        {
            _itemStore = itemStore;
        }

        public Book GetBookById(int id)
        {
            var item = _itemStore.GetItemById(id);

            return Map(item);
        }

        public IEnumerable<Book> GetBooksByIds(IEnumerable<int> ids)
        {
            var items = _itemStore.GetItemsByIds(ids);

            return items.Select(Map);
        }

        public void UpdateBookQuantity(int id, int availableQuantity)
        {
            var item = _itemStore.GetItemById(id);
            item.AvailableQuantity = availableQuantity;

            _itemStore.Save(item);
        }

        private Book Map(ItemModuleItem item)
        {
            return new Book()
            {
                Id = item.Id,
                AvailableQuantity = item.AvailableQuantity,
                Author = item.Author,
                Name = item.Name,
            };
        }
    }
}
