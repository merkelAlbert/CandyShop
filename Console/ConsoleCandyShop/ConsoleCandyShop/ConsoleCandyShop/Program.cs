using System;
using System.Collections.Generic;
using ConsoleCandyShop.Controllers;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.MenuEntries;
using ConsoleCandyShop.Services;

namespace ConsoleCandyShop
{
    class Program
    {
        static void Main(string[] args)
        {
            var ordersController = new OrdersController(new OrdersService());
            var pastriesController = new PastriesController(new PastriesService());
            var usersController = new UsersController(new UsersService());

            var usersMenuEntry = new UsersMenuEntry(usersController);
            var pastriesMenuEntry = new PastriesMenuEntry(pastriesController);
            var ordersMenuEntry = new OrdersMenuEntry(ordersController, usersController, pastriesController);

            var menu = new Menu(new List<Entry>()
            {
                usersMenuEntry.GetEntry(),
                pastriesMenuEntry.GetEntry(),
                ordersMenuEntry.GetEntry()
            });

            menu.Run();
        }
    }
}