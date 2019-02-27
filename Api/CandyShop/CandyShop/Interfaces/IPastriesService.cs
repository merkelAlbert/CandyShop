using CandyShop.DAL.Models;
using CandyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Services
{
    public interface IPastriesService
    {
        Task<Pastry> AddPastry(PastryInfo pastryInfo);
        Task<Pastry> GetPastry(Guid pastryId);
        Task<List<Pastry>> GetPastries();
        Task<Pastry> UpdatePastry(Guid pastryId, PastryInfo pastryInfo);
        Task<Guid> DeletePastry(Guid pastryId);
    }
}
