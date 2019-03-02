using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
using CandyShop.DTO.Pastries;
using CandyShop.DTO.Users;
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
                Compound = pastry.Compound
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

        public async Task<List<PastryModel>> GetPastries()
        {
            var pastries = await _databaseContext.Pastries.ToListAsync();
            var pastryModels = new List<PastryModel>();
            foreach (var pastry in pastries)
            {
                pastryModels.Add(MapPastryModelFromPastry(pastry));
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