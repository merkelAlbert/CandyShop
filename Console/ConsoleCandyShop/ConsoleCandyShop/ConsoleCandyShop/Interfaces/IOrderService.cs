using System.Collections.Generic;
using ConsoleCandyShop.DAL;

namespace ConsoleCandyShop.Interfaces
{
    public interface IOrderService
    {
        Order GetOrder(int orderId);
        List<Order> GetOrders();
        void AddOrder(Order order);
        void UpdateOrder(int orderId, Order order);
        void DeleteOrder(int orderId);
    }
}