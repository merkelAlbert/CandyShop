using System;
using System.Collections.Generic;

namespace CandyShop.DTO.Orders
{
    public class OrderInfo
    {
        public List<Guid> PastriesIds { get; set; }
        public Guid UserId { get; set; }
    }
}
