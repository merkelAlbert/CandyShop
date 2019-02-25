using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Services
{
    [Interceptor("Benchmark")]
    public class OrdersService : IOrdersService
    {
        private readonly Repository _repository;

        public OrdersService(Repository repository)
        {
            _repository= repository;
        }

        public Order GetOrder(int orderId)
        {
            var order = _repository.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                return order;
            }

            throw new InvalidOperationException("Заказа с данным id не существует");
        }

        public List<Order> GetOrders()
        {
            return _repository.Orders;
        }

        public void AddOrder(Order order)
        {
            order.Id = _repository.Orders.Count;
            _repository.Orders.Add(order);
        }

        public void UpdateOrder(int orderId, Order order)
        {
            var storedOrder = _repository.Orders.FirstOrDefault(o => o.Id == orderId);
            if (storedOrder != null)
            {
                storedOrder.User = order.User;
                storedOrder.Pastries = order.Pastries;
            }
            else
            {
                throw new InvalidOperationException("Заказа с данным id не существует");
            }
        }

        public void DeleteOrder(int orderId)
        {
            var storedOrder = _repository.Orders.FirstOrDefault(o => o.Id == orderId);
            if (storedOrder != null)
            {
                _repository.Orders.Remove(storedOrder);
            }
            else
            {
                throw new InvalidOperationException("Заказа с данным id не существует");
            }
        }
    }
}