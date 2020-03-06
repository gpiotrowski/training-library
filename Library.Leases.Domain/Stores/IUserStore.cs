using Library.Leases.Domain.Entities;

namespace Library.Leases.Domain.Stores
{
    public interface IUserStore
    {
        User GetUserById(int id);
        void SaveUser(User user);
    }
}