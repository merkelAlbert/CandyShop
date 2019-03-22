using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CandyShop.DAL.Models.IntermediateModels;

namespace CandyShop.DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public ICollection<OrderPastry> Pastries { get; set; } = new List<OrderPastry>();
        public DateTime CreationDate { get; set; }
    }
}