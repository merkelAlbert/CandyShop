using ConsoleCandyShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCandyShop.DAL;

namespace ConsoleCandyShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly List<User> _usersRepository = new List<User>();

        public User GetUser(int userId)
        {
            var user = _usersRepository.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return user;
            }

            throw new InvalidOperationException("Пользователя с данным id не существует");
        }

        public List<User> GetUsers()
        {
            return _usersRepository;
        }

        public void AddUser(User user)
        {
            user.Id = _usersRepository.Count;
            _usersRepository.Add(user);
        }

        public void UpdateUser(int userId, User user)
        {
            var storedUser = _usersRepository.FirstOrDefault(u => u.Id == userId);
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
            var storedUser = _usersRepository.FirstOrDefault(u => u.Id == userId);
            if (storedUser != null)
            {
                _usersRepository.Remove(storedUser);
            }
            else
            {
                throw new InvalidOperationException("Пользователя с данным id не существует");
            }
        }
    }
}