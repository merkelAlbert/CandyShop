using System;
using CandyShop.DAL.Enums;

namespace CandyShop.DTO.Pastries
{
    public class PastryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PastryType PastryType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Compound { get; set; }
        public int Request { get; set; }
    }
}