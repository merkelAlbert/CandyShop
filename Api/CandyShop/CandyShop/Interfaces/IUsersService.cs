using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.DTO.Users;
using CandyShop.Filters;

namespace CandyShop.Interfaces
{
    public interface IUsersService
    {
        Task<UserModel> AddUser(UserInfo userInfo);
        Task<List<UserModel>> GetUsers(QueryFilter filter);
        Task<UserModel> GetUser(Guid userId);
        Task<UserModel> UpdateUser(Guid userId, UserInfo userInfo);
        Task<Guid> DeleteUser(Guid userId);
    }
}