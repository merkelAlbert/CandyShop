using CandyShop.DTO;
using CandyShop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost]
        public async Task<object> AddOrder([FromBody] OrderInfo orderInfo)
        {
            try
            {
                return await _ordersService.AddOrder(orderInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<object> GetOrders()
        {
            return await _ordersService.GetOrders();
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<object> GetOrder([FromRoute] Guid orderId)
        {
            try
            {
                return await _ordersService.GetOrder(orderId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("{orderId}")]
        public async Task<object> UpdateUser([FromRoute] Guid orderId, [FromBody] OrderInfo orderInfo)
        {
            try
            {
                return await _ordersService.UpdateOrder(orderId, orderInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{orderId}")]
        public async Task<object> DeleteOrder([FromRoute] Guid orderId)
        {
            try
            {
                return await _ordersService.DeleteOrder(orderId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
