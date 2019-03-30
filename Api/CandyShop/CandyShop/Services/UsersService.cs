using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Enums;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.DTO.Orders;
using CandyShop.DTO.Users;
using CandyShop.Filters;
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

        private List<User> ApplyFilter(List<User> users, QueryFilter filter)
        {
            var prop = typeof(User).GetProperty(filter.PropertyName ?? "");
            var filteredUsers = users;
            if (prop != null)
            {
                switch (filter.SortingType)
                {
                    case SortingType.Asc:
                    {
                        filteredUsers = filteredUsers.OrderBy(user => prop.GetValue(user, null))
                            .Take(filter.Count ?? filteredUsers.Count)
                            .ToList();
                        break;
                    }
                    case SortingType.Desc:
                    {
                        filteredUsers = filteredUsers.OrderByDescending(user => prop.GetValue(user, null))
                            .Take(filter.Count ?? filteredUsers.Count)
                            .ToList();
                        break;
                    }
                    case SortingType.Equals:
                    {
                        filteredUsers = filteredUsers.Where(user =>
                                prop.GetValue(user, null).ToString().ToLower() == filter.ValueToEqual.ToLower())
                            .Take(filter.Count ?? filteredUsers.Count)
                            .ToList();
                        break;
                    }
                }
            }
            else
            {
                filteredUsers = filteredUsers.Take(filter.Count ?? filteredUsers.Count).ToList();
            }

            return filteredUsers;
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

        private void UpdateUserFromInfo(ref User user, UserInfo userInfo)
        {
            user.Name = userInfo.Name;
            user.Phone = userInfo.Phone;
        }

        public async Task<UserModel> AddUser(UserInfo userInfo)
        {
            var user = MapUserFromUserInfo(userInfo);
            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();
            return MapUserModelFromUser(user);
        }

        public async Task<List<UserModel>> GetUsers(QueryFilter filter)
        {
            var users = await _databaseContext.Users.ToListAsync();
            var filteredUsers = ApplyFilter(users, filter);
            var userModels = new List<UserModel>();
            foreach (var user in filteredUsers)
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
            UpdateUserFromInfo(ref storedUser, userInfo);
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