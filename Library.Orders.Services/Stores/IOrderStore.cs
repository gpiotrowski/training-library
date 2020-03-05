using System.Collections.Generic;
using Library.Orders.Infrastructure.Data;

namespace Library.Orders.Infrastructure.Stores
{
    public interface IOrderStore
    {
        int GetUserOrderInProgressQty(int userId);
        void PlaceOrder(Order order);
        IEnumerable<Order> GetUserOrders(int userId);
    }
}