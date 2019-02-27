using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;

namespace CandyShop.Interfaces
{
    public interface IUsersService
    {
        Task<User> AddUser(UserInfo userInfo);
        Task<List<User>> GetUsers();
        Task<User> GetUser(Guid userId);
        Task<User> UpdateUser(Guid userId, UserInfo userInfo);
        Task<Guid> DeleteUser(Guid userId);
    }
}