using Library.Orders.Services.Entities;

namespace Library.Orders.Services.Stores
{
    public interface IUserStore
    {
        User GetUserById(int id);
        void SaveUser(User user);
    }
}