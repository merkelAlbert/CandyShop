using ConsoleCandyShop.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCandyShop.Interfaces
{
    public interface IUsersService
    {
        User GetUser(int userId);
        List<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(int userId, User user);
        void DeleteUser(int userId);
    }
}
