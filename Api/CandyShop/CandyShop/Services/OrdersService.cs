using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Order> AddOrder(OrderInfo orderInfo)
        {
            var order = new Order()
            {
                User = orderInfo.User,
                Pastries = orderInfo.Pastries
            };
            _databaseContext.Orders.Add(order);
            await _databaseContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _databaseContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            var order = await _databaseContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            return order ?? throw new InvalidOperationException("Заказа с данным id не существует");
        }

        public async Task<Order> UpdateOrder(Guid orderId, OrderInfo orderInfo)
        {
            var storedOrder = await _databaseContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (storedOrder == null) throw new InvalidOperationException("Заказа с данным id не существует");
            storedOrder.Pastries = orderInfo.Pastries;
            storedOrder.User = orderInfo.User;
            await _databaseContext.SaveChangesAsync();
            return storedOrder;
        }

        public async Task<Guid> DeleteOrder(Guid orderId)
        {
            var storedOrder = await _databaseContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (storedOrder == null) throw new InvalidOperationException("Заказа с данным id не существует");
            _databaseContext.Orders.Remove(storedOrder);
            await _databaseContext.SaveChangesAsync();
            return orderId;
        }
    }
}
