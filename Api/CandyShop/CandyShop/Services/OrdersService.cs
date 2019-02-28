using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL.Models.IntermediateModels;
using CandyShop.DTO.Orders;
using CandyShop.DTO.Pastries;
using CandyShop.DTO.Users;

namespace CandyShop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUsersService _usersService;
        private readonly IPastriesService _pastriesService;

        public OrdersService(DatabaseContext databaseContext, IUsersService usersService,
            IPastriesService pastriesService)
        {
            _databaseContext = databaseContext;
            _usersService = usersService;
            _pastriesService = pastriesService;
        }

        private async Task<Order> MapOrderFromInfo(OrderInfo orderInfo)
        {
            var userModel = await _usersService.GetUser(orderInfo.UserId);
            var order = new Order()
            {
                User = new User()
                {
                    Id = userModel.Id,
                    Name = userModel.Name,
                    Phone = userModel.Phone
                }
            };
            foreach (var pastryId in orderInfo.PastriesIds)
            {
                var pastryModel = await _pastriesService.GetPastry(pastryId);
                order.Pastries.Add(new OrderPastry()
                {
                    Pastry = new Pastry()
                    {
                        Id = pastryModel.Id,
                        Name = pastryModel.Name,
                        Price = pastryModel.Price,
                        Compound = pastryModel.Compound,
                        Description = pastryModel.Description,
                        PastryType = pastryModel.PastryType
                    }
                });
            }

            return order;
        }

        private void UpdateOrderFromInfo(ref Order order, OrderInfo orderInfo)
        {
            order.Pastries = new List<OrderPastry>();
            foreach (var pastryId in orderInfo.PastriesIds)
            {
                var pastryModel = _pastriesService.GetPastry(pastryId).Result;
                order.Pastries.Add(new OrderPastry()
                {
                    Pastry = new Pastry()
                    {
                        Id = pastryModel.Id,
                        Name = pastryModel.Name,
                        Price = pastryModel.Price,
                        Compound = pastryModel.Compound,
                        Description = pastryModel.Description,
                        PastryType = pastryModel.PastryType
                    },
                });
            }
        }

        private OrderModel MapOrderModelFromOrder(Order order)
        {
            var userModel = new UserModel()
            {
                Id = order.User.Id,
                Name = order.User.Name,
                Phone = order.User.Phone
            };
            var pastriesModels = order.Pastries.Select(op => op.Pastry)
                .Select(pastry => new PastryModel()
                {
                    Id = pastry.Id,
                    Name = pastry.Name,
                    Price = pastry.Price,
                    Compound = pastry.Compound,
                    Description = pastry.Description,
                    PastryType = pastry.PastryType
                })
                .ToList();

            var orderModel = new OrderModel()
            {
                Id = order.Id,
                User = userModel,
                Pastries = pastriesModels
            };
            return orderModel;
        }

        public async Task<OrderModel> AddOrder(OrderInfo orderInfo)
        {
            try
            {
                var order = await MapOrderFromInfo(orderInfo);
                _databaseContext.Orders.Add(order);
                await _databaseContext.SaveChangesAsync();
                return MapOrderModelFromOrder(order);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task<List<OrderModel>> GetOrders()
        {
            var orders = await _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries)
                .ThenInclude(p => p.Pastry)
                .ToListAsync();

            var orderModels = new List<OrderModel>();
            foreach (var order in orders)
            {
                orderModels.Add(MapOrderModelFromOrder(order));
            }

            return orderModels;
        }

        public async Task<OrderModel> GetOrder(Guid orderId)
        {
            var order = await _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries)
                .ThenInclude(op => op.Pastry)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                return MapOrderModelFromOrder(order);
            }

            throw new InvalidOperationException("Заказа с данным id не существует");
        }

        public async Task<OrderModel> UpdateOrder(Guid orderId, OrderInfo orderInfo)
        {
            var storedOrder = await _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries)
                .ThenInclude(op => op.Pastry)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (storedOrder == null) throw new InvalidOperationException("Заказа с данным id не существует");
            try
            {
                UpdateOrderFromInfo(ref storedOrder, orderInfo);
                storedOrder.Id = orderId;
                await _databaseContext.SaveChangesAsync();
                return MapOrderModelFromOrder(storedOrder);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task<Guid> DeleteOrder(Guid orderId)
        {
            var storedOrder = await _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries)
                .ThenInclude(p => p.Pastry)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (storedOrder == null) throw new InvalidOperationException("Заказа с данным id не существует");
            _databaseContext.Orders.Remove(storedOrder);
            await _databaseContext.SaveChangesAsync();
            return orderId;
        }
    }
}