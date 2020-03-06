using System.Collections.Generic;
using Library.Leases.Domain.Entities;

namespace Library.Leases.Domain.Stores
{
    public interface ILeaseStore
    {
        int GetUserLeaseInProgressQty(int userId);
        void Lease(Lease lease);
        IEnumerable<Lease> GetUserLeases(int userId);
    }
}