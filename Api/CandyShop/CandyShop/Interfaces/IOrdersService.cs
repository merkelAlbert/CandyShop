using CandyShop.DAL.Models;
using CandyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Interfaces
{
    public interface IOrdersService
    {
        Task<Order> AddOrder(OrderInfo orderInfo);
        Task<Order> GetOrder(Guid orderId);
        Task<List<Order>> GetOrders();
        Task<Order> UpdateOrder(Guid orderId, OrderInfo orderInfo);
        Task<Guid> DeleteOrder(Guid orderId);
    }
}
