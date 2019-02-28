using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.DTO.Users;
using CandyShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<object> AddUser([FromBody] UserInfo userInfo)
        {
            try
            {
                return await _usersService.AddUser(userInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<object> GetUsers()
        {
            return await _usersService.GetUsers();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<object> GetUser([FromRoute] Guid userId)
        {
            try
            {
                return await _usersService.GetUser(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("{userId}")]
        public async Task<object> UpdateUser([FromRoute] Guid userId, [FromBody] UserInfo userInfo)
        {
            try
            {
                return await _usersService.UpdateUser(userId, userInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<object> DeleteUser([FromRoute] Guid userId)
        {
            try
            {
                return await _usersService.DeleteUser(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}