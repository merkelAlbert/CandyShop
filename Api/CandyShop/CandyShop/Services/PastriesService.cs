using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO;
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

        public async Task<Pastry> AddPastry(PastryInfo pastryInfo)
        {
            var pastry = new Pastry()
            {
                Name = pastryInfo.Name,
                PastryType = pastryInfo.PastryType,
                Description = pastryInfo.Description,
                Price = pastryInfo.Price,
                Compound = pastryInfo.Compound
            };
            _databaseContext.Pastries.Add(pastry);
            await _databaseContext.SaveChangesAsync();
            return pastry;
        }

        public async Task<List<Pastry>> GetPastries()
        {
            return await _databaseContext.Pastries.ToListAsync();
        }

        public async Task<Pastry> GetPastry(Guid pastryId)
        {
            var pastry = await _databaseContext.Pastries.FirstOrDefaultAsync(p => p.Id == pastryId);
            return pastry ?? throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
        }

        public async Task<Pastry> UpdatePastry(Guid pastryId, PastryInfo pastryInfo)
        {
            var storedPastry = await _databaseContext.Pastries.FirstOrDefaultAsync(p => p.Id == pastryId);
            if (storedPastry == null) throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            storedPastry.Name = pastryInfo.Name;
            storedPastry.PastryType = pastryInfo.PastryType;
            storedPastry.Description = pastryInfo.Description;
            storedPastry.Price = pastryInfo.Price;
            storedPastry.Compound = pastryInfo.Compound;
            await _databaseContext.SaveChangesAsync();
            return storedPastry;
        }

        public async Task<Guid> DeletePastry(Guid pastryId)
        {
            var storedPastry = await _databaseContext.Users.FirstOrDefaultAsync(p => p.Id == pastryId);
            if (storedPastry == null) throw new InvalidOperationException("Кондитерского изделия с данным id не существует");
            _databaseContext.Pastries.Remove(storedPastry);
            await _databaseContext.SaveChangesAsync();
            return pastryId;
        }
    }
}
