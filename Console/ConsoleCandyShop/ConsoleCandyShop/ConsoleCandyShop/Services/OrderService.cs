using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _ordersRepository = new List<Order>();

        public Order GetOrder(int orderId)
        {
            var order = _ordersRepository.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                return order;
            }

            throw new InvalidOperationException("Заказа с данным id не существует");
        }

        public List<Order> GetOrders()
        {
            return _ordersRepository;
        }

        public void AddOrder(Order order)
        {
            order.Id = _ordersRepository.Count;
            _ordersRepository.Add(order);
        }

        public void UpdateOrder(int orderId, Order order)
        {
            var storedOrder = _ordersRepository.FirstOrDefault(o => o.Id == orderId);
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
            var storedOrder = _ordersRepository.FirstOrDefault(o => o.Id == orderId);
            if (storedOrder != null)
            {
                _ordersRepository.Remove(storedOrder);
            }
            else
            {
                throw new InvalidOperationException("Заказа с данным id не существует");
            }
        }
    }
}