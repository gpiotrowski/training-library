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

        public IEnumerable<ItemDto> GetItemsByIds(IEnumerable<int> ids)
        {
            return _store.GetItemsByIds(ids).Select(x => new ItemDto()
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                Price = x.Price,
                AvailableQuantity = x.AvailableQuantity
            });
        }

        public int GetAvailableItemsQty(int itemId)
        {
            return _store.GetItemById(itemId).AvailableQuantity;
        }

        public void DecreaseItemAvailability(int itemId)
        {
            var item = _store.GetItemById(itemId);
            item.AvailableQuantity--;
            _store.Save(item);
        }
    }
}
