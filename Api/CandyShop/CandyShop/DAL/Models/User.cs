using System;
using System.Collections.Generic;

namespace CandyShop.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}