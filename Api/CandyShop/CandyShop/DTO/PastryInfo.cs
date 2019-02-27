using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.DTO
{
    public class PastryInfo
    {
        public string Name { get; set; }
        public PastryType PastryType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Compound { get; set; }
    }
}
