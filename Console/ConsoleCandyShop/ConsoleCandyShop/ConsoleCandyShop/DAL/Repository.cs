using System.Collections.Generic;

namespace ConsoleCandyShop.DAL
{
    public class Repository
    {
        public List<Pastry> Pastries { get; set; } = new List<Pastry>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}