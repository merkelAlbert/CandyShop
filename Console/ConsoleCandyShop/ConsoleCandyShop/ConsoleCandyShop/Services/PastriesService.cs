using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Services
{
    public class PastriesService : IPastriesService
    {
        private readonly Repository _repository;

        public PastriesService(Repository repository)
        {
            _repository = repository;
        }

        public Pastry GetPastry(int pastryId)
        {
            var pastry = _repository.Pastries.FirstOrDefault(p => p.Id == pastryId);
            if (pastry != null)
            {
                return pastry;
            }

            throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
        }

        public List<Pastry> GetPastries()
        {
            return _repository.Pastries;
        }

        public void AddPastry(Pastry pastry)
        {
            pastry.Id = _repository.Pastries.Count;
            _repository.Pastries.Add(pastry);
        }

        public void UpdatePastry(int pastryId, Pastry pastry)
        {
            var storedPastry = _repository.Pastries.FirstOrDefault(p => p.Id == pastryId);
            if (storedPastry != null)
            {
                storedPastry.Compound = pastry.Compound;
                storedPastry.Description = pastry.Description;
                storedPastry.Name = pastry.Name;
                storedPastry.PastryType = pastry.PastryType;
                storedPastry.Price = pastry.Price;
            }
            else
            {
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            }
        }

        public void DeletePastry(int pastryId)
        {
            var storedPastry = _repository.Pastries.FirstOrDefault(p => p.Id == pastryId);
            if (storedPastry != null)
            {
                _repository.Pastries.Remove(storedPastry);
            }
            else
            {
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            }
        }
    }
}