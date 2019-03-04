using System;

namespace CandyShop.DAL.Models.IntermediateModels
{
    public class OrderPastry
    {
        public Guid Id { get; set; }
        public Guid PastryId { get; set; }
        public Pastry Pastry { get; set; }
        
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Amount { get; set; }
    }
}