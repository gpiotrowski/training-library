using System.Collections.Generic;
using System.Linq;
using Library.Orders.Services.Entities;
using Library.Orders.Services.Stores;

namespace Library.Orders.Infrastructure.Stores
{
    public class OrderStore : IOrderStore
    {
        public List<Order> Orders { get; set; }

        public OrderStore()
        {
            Orders = new List<Order>();
        }

        public int GetUserOrderInProgressQty(int userId)
        {
            return Orders.Count(x => x.UserId == userId && !x.IsReturned);
        }

        public void PlaceOrder(Order order)
        {
            Orders.Add(order);
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return Orders.Where(x => x.UserId == userId);
        }
    }
}