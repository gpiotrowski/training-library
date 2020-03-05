using System.Collections.Generic;
using Library.Orders.Services.Entities;

namespace Library.Orders.Services.Stores
{
    public interface IOrderStore
    {
        int GetUserOrderInProgressQty(int userId);
        void PlaceOrder(Order order);
        IEnumerable<Order> GetUserOrders(int userId);
    }
}