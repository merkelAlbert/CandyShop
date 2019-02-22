using System;
using System.Collections.Generic;

namespace ConsoleCandyShop
{
    public class Menu
    {
        public List<Entry> Entries { get; set; }

        public Menu(List<Entry> entries)
        {
            Entries = entries;
        }

        public void Run()
        {
            while (true)
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    Console.WriteLine($"{i}. {Entries[i].Title}");
                }

                Console.WriteLine($"{Entries.Count}. Выход");

                Console.Write("Введите действие >> ");
                var entryNumber = int.Parse(Console.ReadLine());

                if (entryNumber == Entries.Count)
                {
                    return;
                }

                Console.WriteLine("---");

                for (int i = 0; i < Entries[entryNumber].Handlers.Count; i++)
                {
                    Console.WriteLine($"{i}. {Entries[entryNumber].Handlers[i].Title}");
                }

                Console.WriteLine($"{Entries[entryNumber].Handlers.Count}. Назад");
                Console.WriteLine($"{Entries[entryNumber].Handlers.Count + 1}. Выход");

                Console.Write("Введите действие >> ");
                var handlerNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("---");

                if (handlerNumber == Entries[entryNumber].Handlers.Count)
                {
                    continue;
                }

                if (handlerNumber == Entries[entryNumber].Handlers.Count + 1)
                {
                    return;
                }

                Entries[entryNumber].Handlers[handlerNumber].Action.Invoke();
                Console.WriteLine("---");
            }
        }
    }
}