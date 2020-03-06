﻿using System.Collections.Generic;
using System.Linq;
using Library.Leases.Domain.Entities;
using Library.Leases.Domain.Stores;

namespace Library.Orders.Infrastructure.Stores
{
    public class UserStore : IUserStore
    {
        private List<User> _users { get; set; }

        public UserStore()
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "Jan Kowalski"
                },
                new User()
                {
                    Id = 2,
                    Name = "Katarzyna Nowak"
                }
            };
        }

        public User GetUserById(int id)
        {
            return _users.SingleOrDefault(x => x.Id == id);
        }

        public void SaveUser(User user)
        {
            _users.RemoveAll(x => x.Id == user.Id);
            _users.Add(user);
        }
    }
}