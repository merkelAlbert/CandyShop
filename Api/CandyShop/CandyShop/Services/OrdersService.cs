﻿using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL.Enums;
using CandyShop.DAL.Models.IntermediateModels;
using CandyShop.DTO.OrderPastry;
using CandyShop.DTO.Orders;
using CandyShop.DTO.Pastries;
using CandyShop.DTO.Users;
using CandyShop.Filters;

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

        private List<Order> ApplyFilter(List<Order> orders, QueryFilter filter)
        {
            var filteredOrders = orders;
            if (filter.StartDate != default(DateTime) && filter.EndDate != default(DateTime))
                filteredOrders = filteredOrders.Where(order =>
                        order.CreationDate.Date >= filter.StartDate.Date &&
                        order.CreationDate.Date <= filter.EndDate.Date)
                    .ToList();
            if (filter.PropertyName == "Sum")
            {
                switch (filter.SortingType)
                {
                    case SortingType.Asc:
                    {
                        filteredOrders = filteredOrders.OrderBy(order =>
                                MapOrderModelFromOrder(order).Sum)
                            .Take(filter.Count ?? filteredOrders.Count)
                            .ToList();
                        break;
                    }
                    case SortingType.Desc:
                    {
                        filteredOrders = filteredOrders.OrderByDescending(order =>
                                MapOrderModelFromOrder(order).Sum)
                            .Take(filter.Count ?? filteredOrders.Count)
                            .ToList();
                        break;
                    }
                    case SortingType.Equals:
                    {
                        filteredOrders = filteredOrders.Where(order =>
                                MapOrderModelFromOrder(order).Sum
                                    .ToString() == filter.ValueToEqual)
                            .Take(filter.Count ?? filteredOrders.Count)
                            .ToList();
                        break;
                    }
                }
            }
            else
            {
                var prop = typeof(Order).GetProperty(filter.PropertyName ?? "");
                if (prop != null)
                {
                    switch (filter.SortingType)
                    {
                        case SortingType.Asc:
                        {
                            filteredOrders = filteredOrders.OrderBy(order => prop.GetValue(order, null))
                                .Take(filter.Count ?? filteredOrders.Count)
                                .ToList();
                            break;
                        }
                        case SortingType.Desc:
                        {
                            filteredOrders = filteredOrders.OrderByDescending(order => prop.GetValue(order, null))
                                .Take(filter.Count ?? filteredOrders.Count)
                                .ToList();
                            break;
                        }
                        case SortingType.Equals:
                        {
                            filteredOrders = filteredOrders.Where(order =>
                                    prop.GetValue(order, null).ToString().ToLower() == filter.ValueToEqual.ToLower())
                                .Take(filter.Count ?? filteredOrders.Count)
                                .ToList();
                            break;
                        }
                    }
                }
                else
                {
                    filteredOrders = filteredOrders.Take(filter.Count ?? filteredOrders.Count).ToList();
                }
            }

            return filteredOrders;
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
            foreach (var orderPastryInfo in orderInfo.PastriesInfos)
            {
                var pastryModel = await _pastriesService.GetPastry(orderPastryInfo.PastryId);
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
                    Amount = orderPastryInfo.Amount
                });
            }

            return order;
        }

        private void UpdateOrderFromInfo(ref Order order, OrderInfo orderInfo)
        {
            order.Pastries = new List<OrderPastry>();
            foreach (var orderPastryInfo in orderInfo.PastriesInfos)
            {
                var pastryModel = _pastriesService.GetPastry(orderPastryInfo.PastryId).Result;
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
                    Amount = orderPastryInfo.Amount
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
            var orderPastriesModels = new List<OrderPastryModel>();
            foreach (var orderPastry in order.Pastries)
            {
                var pastryModel = _pastriesService.GetPastry(orderPastry.PastryId).Result;
                orderPastriesModels.Add(new OrderPastryModel()
                {
                    Id = orderPastry.Id,
                    Pastry = pastryModel,
                    Amount = orderPastry.Amount,
                });
            }

            var orderModel = new OrderModel()
            {
                Id = order.Id,
                User = userModel,
                Pastries = orderPastriesModels,
                CreationDate = order.CreationDate,
                Sum = order.Pastries.Sum(pastry =>
                    pastry.Amount * pastry.Pastry.Price)
            };
            return orderModel;
        }

        public async Task<OrderModel> AddOrder(OrderInfo orderInfo)
        {
            try
            {
                var order = await MapOrderFromInfo(orderInfo);
                order.CreationDate = DateTime.Now;
                _databaseContext.Orders.Add(order);
                await _databaseContext.SaveChangesAsync();
                return MapOrderModelFromOrder(order);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task<List<OrderModel>> GetOrders(Guid userId, QueryFilter filter)
        {
            var orders = await _databaseContext.Orders
                .Include(o => o.User)
                .Include(o => o.Pastries)
                .ThenInclude(p => p.Pastry)
                .ToListAsync();

            if (userId != default(Guid))
            {
                orders = orders.Where(order => order.User.Id == userId).ToList();
            }

            var filteredOrders = ApplyFilter(orders, filter);
            var orderModels = new List<OrderModel>();
            foreach (var order in filteredOrders)
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

        public decimal GetSum(List<OrderModel> orders)
        {
            return orders.Sum(order => order.Pastries.Sum(pastry => pastry.Pastry.Price * pastry.Amount));
        }
    }
}