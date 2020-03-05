using Library.Orders.Infrastructure.Data;

namespace Library.Orders.Infrastructure.Stores
{
    public interface IUserStore
    {
        User GetUserById(int id);
        void SaveUser(User user);
    }
}