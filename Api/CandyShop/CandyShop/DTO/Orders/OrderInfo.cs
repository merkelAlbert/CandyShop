using System;
using System.Collections.Generic;
using CandyShop.DTO.OrderPastry;

namespace CandyShop.DTO.Orders
{
    public class OrderInfo
    {
        public List<OrderPastryInfo> PastriesInfos { get; set; }
        public Guid UserId { get; set; }
    }
}
