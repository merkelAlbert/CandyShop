using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<Pastry> Pastries { get; set; }
        public User User { get; set; }
    }
}
