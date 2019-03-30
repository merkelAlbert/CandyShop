using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandyShop.DAL.Models;
using CandyShop.DTO.Pastries;
using CandyShop.Filters;

namespace CandyShop.Interfaces
{
    public interface IPastriesService
    {
        Task<PastryModel> AddPastry(PastryInfo pastryInfo);
        Task<PastryModel> GetPastry(Guid pastryId);
        Task<List<PastryModel>> GetPastries(QueryFilter filter);
        Task<PastryModel> UpdatePastry(Guid pastryId, PastryInfo pastryInfo);
        Task<Guid> DeletePastry(Guid pastryId);
    }
}