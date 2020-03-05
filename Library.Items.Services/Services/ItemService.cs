using System.Collections.Generic;
using System.Linq;
using Library.Items.Infrastructure.Stores;
using Library.Items.Services.Dtos;

namespace Library.Items.Services.Services
{
    public class ItemService
    {
        private readonly ItemStore _store;

        public ItemService(ItemStore store)
        {
            _store = store;
        }

        public IEnumerable<ItemDto> GetItems()
        {
            var allItems = _store.GetItems();

            return allItems.Select(x => new ItemDto()
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                Price = x.Price,
                AvailableQuantity = x.AvailableQuantity
            });
        }
    }
}
