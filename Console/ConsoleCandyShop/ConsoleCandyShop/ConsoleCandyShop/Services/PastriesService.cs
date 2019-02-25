using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Services
{
    [Interceptor("Benchmark")]
    public class PastriesService : IPastriesService
    {
        private readonly DatabaseContext _databaseContext;

        public PastriesService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Pastry GetPastry(int pastryId)
        {
            var pastry = _databaseContext.Pastries.FirstOrDefault(p => p.Id == pastryId);
            if (pastry != null)
            {
                return pastry;
            }

            throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
        }

        public List<Pastry> GetPastries()
        {
            return _databaseContext.Pastries.ToList();
        }

        public void AddPastry(Pastry pastry)
        {
            pastry.Id = _databaseContext.Pastries.ToList().Count;
            _databaseContext.Pastries.Add(pastry);
            _databaseContext.SaveChanges();
        }

        public void UpdatePastry(int pastryId, Pastry pastry)
        {
            var storedPastry = _databaseContext.Pastries.FirstOrDefault(p => p.Id == pastryId);
            if (storedPastry != null)
            {
                storedPastry.Compound = pastry.Compound;
                storedPastry.Description = pastry.Description;
                storedPastry.Name = pastry.Name;
                storedPastry.PastryType = pastry.PastryType;
                storedPastry.Price = pastry.Price;
                _databaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            }
        }

        public void DeletePastry(int pastryId)
        {
            var storedPastry = _databaseContext.Pastries.FirstOrDefault(p => p.Id == pastryId);
            if (storedPastry != null)
            {
                _databaseContext.Pastries.Remove(storedPastry);
                _databaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            }
        }
    }
}