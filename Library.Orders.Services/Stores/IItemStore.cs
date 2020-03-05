using System.Collections.Generic;
using Library.Orders.Services.Entities;

namespace Library.Orders.Services.Stores
{
    public interface IItemStore
    {
        Item GetItemById(int id);
        IEnumerable<Item> GetItemsByIds(IEnumerable<int> ids);
    }
}
