using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.DTO.Users;
using CandyShop.Filters;
using CandyShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IOrdersService _ordersService;

        public UsersController(IUsersService usersService, IOrdersService ordersService)
        {
            _usersService = usersService;
            _ordersService = ordersService;
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
        public async Task<object> GetUsers([FromQuery] QueryFilter filter)
        {
            return await _usersService.GetUsers(filter);
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

        [HttpGet]
        [Route("{userId}/orders")]
        public async Task<object> GetUserOrders([FromRoute] Guid userId, [FromQuery] QueryFilter filter)
        {
            try
            {
                var orders = await _ordersService.GetOrders(userId, filter);
                var sum = _ordersService.GetSum(orders);
                return new
                {
                    orders, sum
                };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}