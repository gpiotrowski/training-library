using System.Collections.Generic;
using System.Linq;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Stores
{
    public class OrderStore
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