using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Services
{
    public class PastriesService : IPastriesService
    {
        private readonly List<Pastry> _pastriesRepository = new List<Pastry>();

        public Pastry GetPastry(int pastryId)
        {
            var pastry = _pastriesRepository.FirstOrDefault(p => p.Id == pastryId);
            if (pastry != null)
            {
                return pastry;
            }

            throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
        }

        public List<Pastry> GetPastries()
        {
            return _pastriesRepository;
        }

        public void AddPastry(Pastry pastry)
        {
            pastry.Id = _pastriesRepository.Count;
            _pastriesRepository.Add(pastry);
        }

        public void UpdatePastry(int pastryId, Pastry pastry)
        {
            var storedPastry = _pastriesRepository.FirstOrDefault(p => p.Id == pastryId);
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
            var storedPastry = _pastriesRepository.FirstOrDefault(p => p.Id == pastryId);
            if (storedPastry != null)
            {
                _pastriesRepository.Remove(storedPastry);
            }
            else
            {
                throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            }
        }
    }
}