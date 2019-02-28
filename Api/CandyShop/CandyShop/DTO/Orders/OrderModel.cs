using System;
using System.Collections.Generic;
using CandyShop.DTO.Pastries;
using CandyShop.DTO.Users;

namespace CandyShop.DTO.Orders
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }
        public List<PastryModel> Pastries { get; set; } = new List<PastryModel>();
    }
}