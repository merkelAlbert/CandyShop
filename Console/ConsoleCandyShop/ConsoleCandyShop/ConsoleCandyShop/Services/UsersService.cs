using ConsoleCandyShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using ConsoleCandyShop.DAL;

namespace ConsoleCandyShop.Services
{
    [Interceptor("Benchmark")]
    public class UsersService : IUsersService
    {
        private readonly DatabaseContext _databaseContext;

        public UsersService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        
        public User GetUser(int userId)
        {
            var user = _databaseContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return user;
            }

            throw new InvalidOperationException("Пользователя с данным id не существует");
        }

        public List<User> GetUsers()
        {
            return _databaseContext.Users.ToList();
        }

        public void AddUser(User user)
        {
            user.Id = _databaseContext.Users.ToList().Count;
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
        }

        public void UpdateUser(int userId, User user)
        {
            var storedUser = _databaseContext.Users.FirstOrDefault(u => u.Id == userId);
            if (storedUser != null)
            {
                storedUser.Name = user.Name;
                storedUser.Phone = user.Phone;
                _databaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Пользователя с данным id не существует");
            }
        }

        public void DeleteUser(int userId)
        {
            var storedUser = _databaseContext.Users.FirstOrDefault(u => u.Id == userId);
            if (storedUser != null)
            {
                _databaseContext.Users.Remove(storedUser);
                _databaseContext.SaveChanges();
                    
            }
            else
            {
                throw new InvalidOperationException("Пользователя с данным id не существует");
            }
        }
    }
}