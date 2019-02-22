using System.Collections.Generic;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.DAL.Enums;

namespace ConsoleCandyShop
{
    public static class Store
    {
        public static Pastry Napoleon = new Pastry(PastryType.Cake, "Наполеон", "", 2500m, "");
        public static Pastry ChockoMuffin = new Pastry(PastryType.Muffin, "Кекс шоколадный", "", 250m, "");
        public static Pastry MilkCookie = new Pastry(PastryType.Cookie, "Топленое молоко", "", 100m, "");
        public static Pastry Praga = new Pastry(PastryType.Cake, "Прага", "", 3000m, "");
    }
}