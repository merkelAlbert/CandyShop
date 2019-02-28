using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.DTO.Users;
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

        private User MapUserFromUserInfo(UserInfo userInfo)
        {
            var user = new User()
            {
                Name = userInfo.Name,
                Phone = userInfo.Phone
            };
            return user;
        }

        private UserModel MapUserModelFromUser(User user)
        {
            var userModel = new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone
            };
            return userModel;
        }

        public async Task<UserModel> AddUser(UserInfo userInfo)
        {
            var user = MapUserFromUserInfo(userInfo);
            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();
            return MapUserModelFromUser(user);
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _databaseContext.Users.ToListAsync();
            var userModels = new List<UserModel>();
            foreach (var user in users)
            {
                userModels.Add(MapUserModelFromUser(user));
            }

            return userModels;
        }

        public async Task<UserModel> GetUser(Guid userId)
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                return MapUserModelFromUser(user);
            }

            throw new InvalidOperationException("Пользователя с данным id не существует");
        }

        public async Task<UserModel> UpdateUser(Guid userId, UserInfo userInfo)
        {
            var storedUser = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (storedUser == null) throw new InvalidOperationException("Пользователя с данным id не существует");
            storedUser = MapUserFromUserInfo(userInfo);
            storedUser.Id = userId;
            await _databaseContext.SaveChangesAsync();
            return MapUserModelFromUser(storedUser);
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