using ConsoleCandyShop.DAL.Enums;

namespace ConsoleCandyShop.DAL
{
    public class Pastry
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public PastryType PastryType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Compound { get; set; }

        public Pastry(PastryType pastryType, string name = "", string description = "", decimal price = 0,
            string compound = "")
        {
            PastryType = pastryType;
            Name = name;
            Description = description;
            Price = price;
            Compound = compound;
        }
    }
}