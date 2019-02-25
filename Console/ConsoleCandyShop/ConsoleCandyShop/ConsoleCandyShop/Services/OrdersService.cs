using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCandyShop.Services
{
    [Interceptor("Benchmark")]
    public class OrdersService : IOrdersService
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Order GetOrder(int orderId)
        {
            var order = _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries)
                .FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                return order;
            }

            throw new InvalidOperationException("Заказа с данным id не существует");
        }

        public List<Order> GetOrders()
        {
            return _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries).ToList();
        }

        public void AddOrder(Order order)
        {
            order.Id = _databaseContext.Orders.ToList().Count;
            _databaseContext.Orders.Add(order);
            _databaseContext.SaveChanges();
        }

        public void UpdateOrder(int orderId, Order order)
        {
            var storedOrder = _databaseContext.Orders
                .FirstOrDefault(o => o.Id == orderId);
            if (storedOrder != null)
            {
                storedOrder.User = order.User;
                storedOrder.Pastries = order.Pastries;
                _databaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Заказа с данным id не существует");
            }
        }

        public void DeleteOrder(int orderId)
        {
            var storedOrder = _databaseContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (storedOrder != null)
            {
                _databaseContext.Orders.Remove(storedOrder);
                _databaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Заказа с данным id не существует");
            }
        }
    }
}