using System.Collections.Generic;
using System.Linq;
using Library.Orders.Services.Entities;
using Library.Orders.Services.Stores;

using ItemModuleItemStore = Library.Items.Infrastructure.Stores.ItemStore;
using ItemModuleItem = Library.Items.Infrastructure.Data.Item;

namespace Library.Orders.Infrastructure.Stores
{
    public class ItemStore : IItemStore
    {
        private readonly ItemModuleItemStore _itemStore;

        public ItemStore(ItemModuleItemStore itemStore)
        {
            _itemStore = itemStore;
        }

        public Item GetItemById(int id)
        {
            var item = _itemStore.GetItemById(id);

            return Map(item);
        }

        public IEnumerable<Item> GetItemsByIds(IEnumerable<int> ids)
        {
            var items = _itemStore.GetItemsByIds(ids);

            return items.Select(Map);
        }

        private Item Map(ItemModuleItem item)
        {
            return new Item()
            {
                Id = item.Id,
                AvailableQuantity = item.AvailableQuantity,
                Author = item.Author,
                Name = item.Name,
            };
        }
    }
}
