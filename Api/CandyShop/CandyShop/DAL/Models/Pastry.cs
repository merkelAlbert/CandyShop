﻿using CandyShop.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL.Models.IntermediateModels;

namespace CandyShop.DAL.Models
{
    public class Pastry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PastryType PastryType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Compound { get; set; }
        public ICollection<OrderPastry> Orders { get; set; } = new List<OrderPastry>();
    }
}