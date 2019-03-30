using System;
using System.Collections.Generic;
using CandyShop.DTO.OrderPastry;
using CandyShop.DTO.Pastries;
using CandyShop.DTO.Users;

namespace CandyShop.DTO.Orders
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }
        public List<OrderPastryModel> Pastries { get; set; } = new List<OrderPastryModel>();
        public DateTime CreationDate { get; set; }
        public decimal Sum { get; set; }
    }
}