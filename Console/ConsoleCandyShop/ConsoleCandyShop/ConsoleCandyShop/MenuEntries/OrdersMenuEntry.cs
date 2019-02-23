using ConsoleCandyShop.Controllers;
using ConsoleCandyShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCandyShop.MenuEntries
{
    public class OrdersMenuEntry
    {
        private readonly OrdersController _ordersController;
        private readonly UsersController _usersController;
        private readonly PastriesController _pastriesController;

        public OrdersMenuEntry(OrdersController ordersController, UsersController usersController, PastriesController pastriesController)
        {
            _ordersController = ordersController;
            _usersController = usersController;
            _pastriesController = pastriesController;
        }

        public Entry GetEntry()
        {
            var entry = new Entry("Заказы", new List<Handler>()
            {
                new Handler("Добавить заказ", () =>
                {
                    foreach (var userItem in _usersController.GetUsers())
                    {
                        Console.WriteLine($"{userItem.Id} | {userItem.Name} | {userItem.Phone}");
                    }
                    Console.Write("id пользователя >> ");
                    var userId = int.Parse(Console.ReadLine());
                    var user = _usersController.GetUser(userId);
                    if (user != null)
                    {
                        
                            foreach (var pastryItem in _pastriesController.GetPastries())
                            {
                                Console.WriteLine(
                                    $"{pastryItem.Id} | {pastryItem.Name} | " +
                                    $"{pastryItem.PastryType.ToString()} | " +
                                    $"{pastryItem.Description} | " +
                                    $"{pastryItem.Price} | " +
                                    $"{pastryItem.Compound}");
                            }
                            var pastriesList = new List<Pastry>();
                            ConsoleKeyInfo input = new ConsoleKeyInfo();
                            while (input.Key != ConsoleKey.N)
                            {
                                Console.Write("id кондитерского изделия >> ");
                                var pastryId = int.Parse(Console.ReadLine());
                                var pastry = _pastriesController.GetPastry(pastryId);
                                if (pastry != null)
                                {
                                    pastriesList.Add(pastry);
                                    Console.Write("Ещё (*/N) >> ");
                                    input = Console.ReadKey();
                                    Console.WriteLine();   
                                }
                            }
                            var order = new Order(user, pastriesList);
                            _ordersController.AddOrder(order);
                    }
                }),
                new Handler("Получить заказы", () =>
                {
                    foreach (var order in _ordersController.GetOrders())
                    {
                        Console.WriteLine(order.Id);
                        var user = order.User;
                        Console.WriteLine($"{user.Id} | {user.Name} | {user.Phone}");
                        foreach (var pastryItem in order.Pastries)
                        {
                            Console.WriteLine(
                                    $"{pastryItem.Id} | {pastryItem.Name} | " +
                                    $"{pastryItem.PastryType.ToString()} | " +
                                    $"{pastryItem.Description} | " +
                                    $"{pastryItem.Price} | " +
                                    $"{pastryItem.Compound}");
                        }
                        Console.WriteLine();
                    }
                }),
                new Handler("Получить заказ", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    var order = _ordersController.GetOrder(id);
                    if (order != null)
                    {
                        Console.WriteLine(order.Id);
                        var user = order.User;
                        Console.WriteLine($"{user.Id} | {user.Name} | {user.Phone}");
                        foreach (var pastryItem in order.Pastries)
                        {
                            Console.WriteLine(
                                    $"{pastryItem.Id} | {pastryItem.Name} | " +
                                    $"{pastryItem.PastryType.ToString()} | " +
                                    $"{pastryItem.Description} | " +
                                    $"{pastryItem.Price} | " +
                                    $"{pastryItem.Compound}");
                        }
                    }
                }),
                new Handler("Изменить заказ", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    var storedOrder = _ordersController.GetOrder(id);
                    if (storedOrder != null)
                    {
                        foreach (var pastryItem in _pastriesController.GetPastries())
                        {
                            Console.WriteLine(
                                    $"{pastryItem.Id} | {pastryItem.Name} | " +
                                    $"{pastryItem.PastryType.ToString()} | " +
                                    $"{pastryItem.Description} | " +
                                    $"{pastryItem.Price} | " +
                                    $"{pastryItem.Compound}");
                        }
                        var pastriesList = new List<Pastry>();
                        ConsoleKeyInfo input = new ConsoleKeyInfo();
                            while (input.Key != ConsoleKey.N)
                            {
                                Console.Write("id кондитерского изделия >> ");
                                var pastryId = int.Parse(Console.ReadLine());
                                var pastry = _pastriesController.GetPastry(pastryId);
                                if (pastry != null)
                                {
                                    pastriesList.Add(pastry);
                                    Console.Write("Ещё (Y/N) >> ");
                                    input = Console.ReadKey();
                                    Console.WriteLine();
                                }
                            }
                            var order = new Order(storedOrder.User, pastriesList);
                            _ordersController.UpdateOrder(id, order);
                        }                
                }),
                new Handler("Удалить заказ", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    if (_ordersController.GetOrder(id) != null)
                    {
                        _ordersController.DeleteOrder(id);
                    }
                }),
            });
            return entry;
        }
    }
}
