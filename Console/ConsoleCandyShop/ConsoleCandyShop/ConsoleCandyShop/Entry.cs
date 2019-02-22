using System;
using System.Collections.Generic;

namespace ConsoleCandyShop
{
    public class Entry
    {
        public string Title { get; set; }
        public List<Handler> Handlers { get; set; }

        public Entry(string title, List<Handler> handlers)
        {
            Title = title;
            Handlers = handlers;
        }
    }
}