using System;

namespace ConsoleCandyShop
{
    public class Handler
    {
        public string Title { get; set; }
        public Action Action { get; set; }

        public Handler(string title, Action action)
        {
            Title = title;
            Action = action;
        }
    }
}