using System;
using System.Collections.Generic;
using ConsoleCandyShop.Controllers;
using ConsoleCandyShop.DAL;

namespace ConsoleCandyShop.MenuEntries
{
    public class UsersMenuEntry
    {
        private readonly UsersController _usersController;

        public UsersMenuEntry(UsersController usersController)
        {
            _usersController = usersController;
        }

        public Entry GetEntry()
        {
            var entry = new Entry("Пользователи", new List<Handler>()
            {
                new Handler("Добавить пользователя", () =>
                {
                    Console.Write("name >> ");
                    var name = Console.ReadLine();
                    Console.Write("phone >> ");
                    var phone = Console.ReadLine();
                    var user = new User(name, phone);
                    _usersController.AddUser(user);
                }),
                new Handler("Получить пользователей", () =>
                {
                    foreach (var user in _usersController.GetUsers())
                    {
                        Console.WriteLine($"{user.Id} {user.Name} {user.Name}");
                    }
                }),
                new Handler("Получить пользователя", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    var user = _usersController.GetUser(id);
                    if (user != null)
                    {
                        Console.WriteLine($"{user.Id} {user.Name} {user.Name}");
                    }
                }),
                new Handler("Изменить пользователя", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    if (_usersController.GetUser(id) != null)
                    {
                        Console.Write("name >> ");
                        var name = Console.ReadLine();
                        Console.Write("phone >> ");
                        var phone = Console.ReadLine();
                        var user = new User(name, phone);
                        _usersController.UpdateUser(id, user);
                    }
                }),
                new Handler("Удалить пользователя", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    if (_usersController.GetUser(id) != null)
                    {
                        _usersController.DeleteUser(id);
                    }
                }),
            });
            return entry;
        }
    }
}