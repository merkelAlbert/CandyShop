using System;
using System.Collections.Generic;
using ConsoleCandyShop.Controllers;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.DAL.Enums;

namespace ConsoleCandyShop.MenuEntries
{
    public class PastriesMenuEntry
    {
        private readonly PastriesController _pastriesController;

        public PastriesMenuEntry(PastriesController pastriesController)
        {
            _pastriesController = pastriesController;
        }

        public Entry GetEntry()
        {
            var entry = new Entry("Кондитерские изделия", new List<Handler>()
            {
                new Handler("Добавить кондитерское изделие", () =>
                {
                    Console.Write("name >> ");
                    var name = Console.ReadLine();

                    var types = Enum.GetNames(typeof(PastryType));
                    for (int i = 0; i < types.Length; i++)
                    {
                        Console.WriteLine($"{i}. {types[i]}");
                    }

                    Console.Write("type >> ");
                    var type = int.Parse(Console.ReadLine());
                    Console.Write("description >> ");
                    var description = Console.ReadLine();
                    Console.Write("price >> ");
                    var price = decimal.Parse(Console.ReadLine());
                    Console.Write("compound >> ");
                    var compound = Console.ReadLine();

                    var pastry = new Pastry((PastryType) type, name, description, price, compound);
                    _pastriesController.AddPastry(pastry);
                }),
                new Handler("Получить кондитерские изделия", () =>
                {
                    foreach (var pastry in _pastriesController.GetPastries())
                    {
                        Console.WriteLine(
                            $"{pastry.Id} | {pastry.Name} | " +
                            $"{pastry.PastryType.ToString()} | " +
                            $"{pastry.Description} | " +
                            $"{pastry.Price} | " +
                            $"{pastry.Compound}");
                    }
                }),
                new Handler("Получить кондитерское изделие", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    var pastry = _pastriesController.GetPastry(id);
                    if (pastry != null)
                    {
                        Console.WriteLine(
                            $"{pastry.Id} | {pastry.Name} | " +
                            $"{pastry.PastryType.ToString()} | " +
                            $"{pastry.Description} | " +
                            $"{pastry.Price} | " +
                            $"{pastry.Compound}");
                    }
                }),
                new Handler("Изменить кондитерское изделие", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    if (_pastriesController.GetPastry(id) != null)
                    {
                        Console.Write("name >> ");
                        var name = Console.ReadLine();

                        var types = Enum.GetNames(typeof(PastryType));
                        for (int i = 0; i < types.Length; i++)
                        {
                            Console.WriteLine($"{i}. {types[i]}");
                        }

                        Console.Write("type >> ");
                        var type = int.Parse(Console.ReadLine());
                        Console.Write("description >> ");
                        var description = Console.ReadLine();
                        Console.Write("price >> ");
                        var price = decimal.Parse(Console.ReadLine());
                        Console.Write("compound >> ");
                        var compound = Console.ReadLine();

                        var pastry = new Pastry((PastryType) type, name, description, price, compound);
                        _pastriesController.UpdatePastry(id, pastry);
                    }
                }),
                new Handler("Удалить кондитерское изделие", () =>
                {
                    Console.Write("id >> ");
                    var id = int.Parse(Console.ReadLine());
                    if (_pastriesController.GetPastry(id) != null)
                    {
                        _pastriesController.DeletePastry(id);
                    }
                }),
            });
            return entry;
        }
    }
}