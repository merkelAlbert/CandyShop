using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Enums;
using CandyShop.DAL.Models;
using CandyShop.DAL.Models.IntermediateModels;
using CandyShop.DTO;
using CandyShop.DTO.Pastries;
using CandyShop.DTO.Users;
using CandyShop.Filters;
using CandyShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandyShop.Services
{
    public class PastriesService : IPastriesService
    {
        private readonly DatabaseContext _databaseContext;

        public PastriesService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        private int GetRequestByPeriod(List<Pastry> pastries, Guid pastryId, DateTime startDate, DateTime endDate)
        {
            var orders = pastries.SelectMany(pastry => pastry.Orders);
            if (startDate != default(DateTime) && endDate != default(DateTime))
                orders = orders.Where(orderPastry =>
                        orderPastry.Order.CreationDate.Date >= startDate.Date &&
                        orderPastry.Order.CreationDate.Date <= endDate.Date)
                    .ToList();

            var groupedOrders = orders.GroupBy(o => o.PastryId).ToList();
            var resultOrders = new List<OrderPastry>();
            foreach (var groupedOrder in groupedOrders)
            {
                var orderPastry = orders.First(o => o.PastryId == groupedOrder.Key);
                var copy = new OrderPastry()
                {
                    Id = orderPastry.Id,
                    Order = orderPastry.Order,
                    Amount = 0,
                    Pastry = orderPastry.Pastry,
                    OrderId = orderPastry.OrderId,
                    PastryId = orderPastry.PastryId
                };

                foreach (var groupedOrderPastry in groupedOrder)
                {
                    copy.Amount += groupedOrderPastry.Amount;
                }

                resultOrders.Add(copy);
            }

            var amount = resultOrders.First(order => order.PastryId == pastryId).Amount;
            return amount;
        }

        private List<Pastry> ApplyFilter(List<Pastry> pastries, QueryFilter filter)
        {
            var orders = pastries.SelectMany(pastry => pastry.Orders);
            if (filter.StartDate != default(DateTime) && filter.EndDate != default(DateTime))
                orders = orders.Where(orderPastry =>
                        orderPastry.Order.CreationDate.Date >= filter.StartDate.Date &&
                        orderPastry.Order.CreationDate.Date <= filter.EndDate.Date)
                    .ToList();

            var groupedOrders = orders.GroupBy(o => o.PastryId).ToList();
            var resultOrders = new List<OrderPastry>();
            foreach (var groupedOrder in groupedOrders)
            {
                var orderPastry = orders.First(o => o.PastryId == groupedOrder.Key);
                resultOrders.Add(orderPastry);
            }

            var filteredPastries = resultOrders.Select(o => o.Pastry).ToList();
            if (filter.PropertyName == "Request")
            {
                switch (filter.SortingType)
                {
                    case SortingType.Asc:
                    {
                        if (filter.StartDate != default(DateTime) && filter.EndDate != default(DateTime))
                            filteredPastries = filteredPastries
                                .OrderBy(pastry => pastry.Orders.Where(orderPastry =>
                                        orderPastry.Order.CreationDate.Date >= filter.StartDate.Date &&
                                        orderPastry.Order.CreationDate.Date <= filter.EndDate.Date)
                                    .Sum(orderPastry => orderPastry.Amount))
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                        else
                            filteredPastries = filteredPastries
                                .OrderBy(pastry => pastry.Orders
                                    .Sum(orderPastry => orderPastry.Amount))
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                        break;
                    }
                    case SortingType.Desc:
                    {
                        if (filter.StartDate != default(DateTime) && filter.EndDate != default(DateTime))
                            filteredPastries = filteredPastries
                                .OrderByDescending(pastry => pastry.Orders.Where(orderPastry =>
                                        orderPastry.Order.CreationDate.Date >= filter.StartDate.Date &&
                                        orderPastry.Order.CreationDate.Date <= filter.EndDate.Date)
                                    .Sum(orderPastry => orderPastry.Amount))
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                        else
                            filteredPastries = filteredPastries
                                .OrderByDescending(pastry => pastry.Orders
                                    .Sum(orderPastry => orderPastry.Amount))
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                        break;
                    }
                    case SortingType.Equals:
                    {
                        filteredPastries = filteredPastries.Where(pastry =>
                                pastry.Orders.Sum(orderPastry => orderPastry.Amount).ToString() == filter.ValueToEqual)
                            .Take(filter.Count ?? filteredPastries.Count)
                            .ToList();
                        break;
                    }
                }
            }
            else
            {
                var prop = typeof(Pastry).GetProperty(filter.PropertyName ?? "");
                if (prop != null)
                {
                    switch (filter.SortingType)
                    {
                        case SortingType.Asc:
                        {
                            filteredPastries = pastries.OrderBy(pastry => prop.GetValue(pastry, null))
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                            break;
                        }
                        case SortingType.Desc:
                        {
                            filteredPastries = pastries.OrderByDescending(pastry => prop.GetValue(pastry, null))
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                            break;
                        }
                        case SortingType.Equals:
                        {
                            filteredPastries = pastries.Where(pastry =>
                                    prop.GetValue(pastry, null).ToString().ToLower() == filter.ValueToEqual.ToLower())
                                .Take(filter.Count ?? filteredPastries.Count)
                                .ToList();
                            break;
                        }
                    }
                }
                else
                {
                    filteredPastries = pastries.Take(filter.Count ?? filteredPastries.Count).ToList();
                }
            }

            return filteredPastries;
        }

        private Pastry MapPastryFromInfo(PastryInfo pastryInfo)
        {
            var pastry = new Pastry()
            {
                Name = pastryInfo.Name,
                PastryType = pastryInfo.PastryType,
                Description = pastryInfo.Description,
                Price = pastryInfo.Price,
                Compound = pastryInfo.Compound
            };
            return pastry;
        }

        private void UpdatePastryFromInfo(ref Pastry pastry, PastryInfo pastryInfo)
        {
            pastry.Name = pastryInfo.Name;
            pastry.PastryType = pastryInfo.PastryType;
            pastry.Description = pastryInfo.Description;
            pastry.Price = pastryInfo.Price;
            pastry.Compound = pastryInfo.Compound;
        }

        private PastryModel MapPastryModelFromPastry(Pastry pastry)
        {
            var pastryModel = new PastryModel()
            {
                Id = pastry.Id,
                Name = pastry.Name,
                PastryType = pastry.PastryType,
                Description = pastry.Description,
                Price = pastry.Price,
                Compound = pastry.Compound,
            };
            return pastryModel;
        }

        public async Task<PastryModel> AddPastry(PastryInfo pastryInfo)
        {
            var pastry = MapPastryFromInfo(pastryInfo);
            _databaseContext.Pastries.Add(pastry);
            await _databaseContext.SaveChangesAsync();
            return MapPastryModelFromPastry(pastry);
        }

        public async Task<List<PastryModel>> GetPastries(QueryFilter filter)
        {
            var pastries = await _databaseContext.Pastries
                .Include(pastry => pastry.Orders)
                .ThenInclude(orderPastry => orderPastry.Order).ToListAsync();
            var filteredPastries = ApplyFilter(pastries, filter);
            var pastryModels = new List<PastryModel>();
            foreach (var pastry in filteredPastries)
            {
                var pastryModel = MapPastryModelFromPastry(pastry);
                pastryModel.Request = GetRequestByPeriod(filteredPastries, pastry.Id, filter.StartDate, filter.EndDate);
                pastryModels.Add(pastryModel);
            }

            return pastryModels;
        }

        public async Task<PastryModel> GetPastry(Guid pastryId)
        {
            var pastry = await _databaseContext.Pastries.FirstOrDefaultAsync(p => p.Id == pastryId);
            if (pastry != null)
            {
                return MapPastryModelFromPastry(pastry);
            }

            throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
        }

        public async Task<PastryModel> UpdatePastry(Guid pastryId, PastryInfo pastryInfo)
        {
            var storedPastry = await _databaseContext.Pastries.FirstOrDefaultAsync(p => p.Id == pastryId);
            if (storedPastry == null)
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            UpdatePastryFromInfo(ref storedPastry, pastryInfo);
            await _databaseContext.SaveChangesAsync();
            return MapPastryModelFromPastry(storedPastry);
        }

        public async Task<Guid> DeletePastry(Guid pastryId)
        {
            var storedPastry = await _databaseContext.Pastries.FirstOrDefaultAsync(p => p.Id == pastryId);
            if (storedPastry == null)
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            _databaseContext.Pastries.Remove(storedPastry);
            await _databaseContext.SaveChangesAsync();
            return pastryId;
        }
    }
}