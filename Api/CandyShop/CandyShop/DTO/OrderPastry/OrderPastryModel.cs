using System;
using CandyShop.DTO.Pastries;

namespace CandyShop.DTO.OrderPastry
{
    public class OrderPastryModel
    {
        public Guid Id { get; set; }
        public PastryModel Pastry { get; set; }
        public int Amount { get; set; }
    }
}