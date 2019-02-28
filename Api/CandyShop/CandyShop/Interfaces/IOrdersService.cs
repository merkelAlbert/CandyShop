using CandyShop.DAL.Models;
using CandyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DTO.Orders;

namespace CandyShop.Interfaces
{
    public interface IOrdersService
    {
        Task<OrderModel> AddOrder(OrderInfo orderInfo);
        Task<OrderModel> GetOrder(Guid orderId);
        Task<List<OrderModel>> GetOrders();
        Task<OrderModel> UpdateOrder(Guid orderId, OrderInfo orderInfo);
        Task<Guid> DeleteOrder(Guid orderId);
    }
}
