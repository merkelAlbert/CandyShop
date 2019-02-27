using CandyShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.DTO
{
    public class OrderInfo
    {
        public List<Pastry> Pastries { get; set; }
        public User User { get; set; }
    }
}
