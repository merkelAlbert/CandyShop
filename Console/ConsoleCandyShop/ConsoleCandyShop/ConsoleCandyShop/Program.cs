using System;
using System.Collections.Generic;
using ConsoleCandyShop.Controllers;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Services;

namespace ConsoleCandyShop
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderController = new OrderController(new OrderService());

            var customer = new User("Альберт", "89106051944");
            var basket = new List<Pastry>();
            basket.Add(Store.Napoleon);
            basket.Add(Store.Praga);
            var order = new Order(basket, customer);
            var order1 = new Order(basket, customer);

            orderController.AddOrder(order);
            orderController.AddOrder(order1);
            var orders = orderController.GetOrders();
            foreach (var orderItem in orders)
            {
                Console.WriteLine(orderItem.Id);
                foreach (var pastry in orderItem.Pastries)
                {
                    Console.WriteLine(pastry.Name);
                }

                Console.WriteLine(order.User.Name);
                Console.WriteLine();
            }
        }
    }
}