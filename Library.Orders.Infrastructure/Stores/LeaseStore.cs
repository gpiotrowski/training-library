using System.Collections.Generic;
using System.Linq;
using Library.Leases.Domain.Entities;
using Library.Leases.Domain.Stores;

namespace Library.Orders.Infrastructure.Stores
{
    public class LeaseStore : ILeaseStore
    {
        public List<Lease> Orders { get; set; }

        public LeaseStore()
        {
            Orders = new List<Lease>();
        }

        public int GetUserLeaseInProgressQty(int userId)
        {
            return Orders.Count(x => x.UserId == userId && !x.IsReturned);
        }

        public void Lease(Lease lease)
        {
            Orders.Add(lease);
        }

        public IEnumerable<Lease> GetUserLeases(int userId)
        {
            return Orders.Where(x => x.UserId == userId);
        }
    }
}