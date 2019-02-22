using System.Collections.Generic;

namespace ConsoleCandyShop.DAL
{
    public class Order
    {
        public int Id { get; set; }
        public List<Pastry> Pastries { get; set; }
        public User User { get; set; }

        public Order(List<Pastry> pastries, User user)
        {
            Pastries = pastries;
            User = user;
        }
    }
}