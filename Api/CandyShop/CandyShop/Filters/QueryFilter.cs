using System;
using CandyShop.DAL.Enums;
using CandyShop.Interfaces;

namespace CandyShop.Filters
{
    public class QueryFilter
    {
        public string PropertyName { get; set; }
        public SortingType SortingType { get; set; }
        public string ValueToEqual { get; set; }
        public int? Count { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}