﻿using System;
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
            var ordersController = new OrdersController(new OrdersService());
            var pastriesController = new PastriesController(new PastriesService());


            var customer = new User("Альберт", "89106051944");
            var basket = new List<Pastry>();
            basket.Add(Store.Napoleon);
            basket.Add(Store.Praga);
            var order = new Order(basket, customer);
            var order1 = new Order(basket, customer);

            var pastry = new Pastry(DAL.Enums.PastryType.Cookie,"cookie","",123m,"");
            var pastry1 = new Pastry(DAL.Enums.PastryType.Cookie, "cookie1", "", 123m, "");
            pastriesController.AddPastry(pastry);
            pastriesController.AddPastry(pastry1);
            foreach (var pastryitem in pastriesController.GetPastries())
            {
                Console.WriteLine(pastryitem.Id);
                Console.WriteLine(pastryitem.Name);
                Console.WriteLine();
            }
            Console.WriteLine("-----");
            pastriesController.DeletePastry(0);
            foreach (var pastryitem in pastriesController.GetPastries())
            {
                Console.WriteLine(pastryitem.Id);
                Console.WriteLine(pastryitem.Name);
                Console.WriteLine();
            }
            //ordersController.AddOrder(order);
            //ordersController.AddOrder(order1);
            //var orders = ordersController.GetOrders();
            //foreach (var orderItem in orders)
            //{
            //    Console.WriteLine(orderItem.Id);
            //    foreach (var pastryItem in orderItem.Pastries)
            //    {
            //        Console.WriteLine(pastryItem.Name);
            //    }

            //    Console.WriteLine(order.User.Name);
            //    Console.WriteLine();
            //}
            Console.ReadKey();
        }
    }
}