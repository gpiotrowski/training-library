using System.Collections.Generic;
using System.Linq;
using Library.Controllers;
using Library.Data;

namespace Library.Stores
{
    public class ItemStore
    {
        private List<Item> _items { get; set; }
        public ItemStore()
        {
            _items = new List<Item>()
            {
                new Item()
                {
                    Id =  1,
                    Name = "Book A",
                    Author = "Author A",
                    Price = 30.99M,
                    AvailableQuantity = 5
                }
            };
        }

        public List<Item> GetItems()
        {
            return _items;
        }

        public Item GetItemById(int id)
        {
            return _items.SingleOrDefault(x => x.Id == id);
        }

        public List<Item> GetItemsByIds(IEnumerable<int> ids)
        {
            return _items.FindAll(x => ids.Contains(x.Id));
        }
    }
}