using System;
using System.Collections.Generic;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Controllers
{
     public class UsersController
    {
        private IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        public void AddUser(User user)
        {
            try
            {
                _userService.AddUser(user);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при создании пользователя: {e.Message}");
            }
        }

        public User GetUser(int id)
        {
            try
            {
                return _userService.GetUser(id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при получении пользователя: {e.Message}");
                return null;
            }
        }

        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        public void UpdateUser(int id, User user)
        {
            try
            {
                _userService.UpdateUser(id, user);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при изменении пользователя: {e.Message}");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при удалении пользователя: {e.Message}");
            }
        }
    }
}
