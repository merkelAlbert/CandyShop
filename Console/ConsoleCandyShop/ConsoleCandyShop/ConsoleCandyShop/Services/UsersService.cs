using ConsoleCandyShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCandyShop.DAL;

namespace ConsoleCandyShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly Repository _repository;

        public UsersService(Repository repository)
        {
            _repository = repository;
        }

        public User GetUser(int userId)
        {
            var user = _repository.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return user;
            }

            throw new InvalidOperationException("Пользователя с данным id не существует");
        }

        public List<User> GetUsers()
        {
            return _repository.Users;
        }

        public void AddUser(User user)
        {
            user.Id = _repository.Users.Count;
            _repository.Users.Add(user);
        }

        public void UpdateUser(int userId, User user)
        {
            var storedUser = _repository.Users.FirstOrDefault(u => u.Id == userId);
            if (storedUser != null)
            {
                storedUser.Name = user.Name;
                storedUser.Phone = user.Phone;
            }
            else
            {
                throw new InvalidOperationException("Пользователя с данным id не существует");
            }
        }

        public void DeleteUser(int userId)
        {
            var storedUser = _repository.Users.FirstOrDefault(u => u.Id == userId);
            if (storedUser != null)
            {
                _repository.Users.Remove(storedUser);
            }
            else
            {
                throw new InvalidOperationException("Пользователя с данным id не существует");
            }
        }
    }
}