using System;
using System.Collections.Generic;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Controllers
{
    public class OrderController
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void AddOrder(Order order)
        {
            try
            {
                _orderService.AddOrder(order);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при создании заказа: {e.Message}");
            }
        }

        public Order GetOrder(int id)
        {
            try
            {
                return _orderService.GetOrder(id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при получении заказа: {e.Message}");
                return null;
            }
        }

        public List<Order> GetOrders()
        {
            return _orderService.GetOrders();
        }

        public void UpdateOrder(int id, Order order)
        {
            try
            {
                _orderService.UpdateOrder(id, order);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при изменении заказа: {e.Message}");
            }
        }

        public void DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при удалении заказа: {e.Message}");
            }
        }
    }
}