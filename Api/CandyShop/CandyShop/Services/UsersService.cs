using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandyShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly DatabaseContext _databaseContext;

        public UsersService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<User> AddUser(UserInfo userInfo)
        {
            var user = new User()
            {
                Name = userInfo.Name,
                Phone = userInfo.Phone
            };
            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _databaseContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(Guid userId)
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user ?? throw new InvalidOperationException("Пользователя с данным id не существует");
        }

        public async Task<User> UpdateUser(Guid userId, UserInfo userInfo)
        {
            var storedUser = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (storedUser == null) throw new InvalidOperationException("Пользователя с данным id не существует");
            storedUser.Name = userInfo.Name;
            storedUser.Phone = userInfo.Phone;
            await _databaseContext.SaveChangesAsync();
            return storedUser;
        }

        public async Task<Guid> DeleteUser(Guid userId)
        {
            var storedUser = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (storedUser == null) throw new InvalidOperationException("Пользователя с данным id не существует");
            _databaseContext.Users.Remove(storedUser);
            await _databaseContext.SaveChangesAsync();
            return userId;
        }
    }
}