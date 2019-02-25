using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ConsoleCandyShop.Controllers;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Installers;
using ConsoleCandyShop.Interfaces;
using ConsoleCandyShop.MenuEntries;
using ConsoleCandyShop.Services;

namespace ConsoleCandyShop
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new RepositoryInstaller(),
                new InterceptorsInstaller(),
                new ServicesInstaller(),
                new ControllersInstaller(),
                new MenuEntriesInstaller());

            var usersMenuEntry = container.Resolve<UsersMenuEntry>();
            var pastriesMenuEntry = container.Resolve<PastriesMenuEntry>();
            var ordersMenuEntry = container.Resolve<OrdersMenuEntry>();

            var menu = new Menu(new List<Entry>()
            {
                usersMenuEntry.GetEntry(),
                pastriesMenuEntry.GetEntry(),
                ordersMenuEntry.GetEntry()
            });

            menu.Run();
            container.Dispose();
        }
    }
}