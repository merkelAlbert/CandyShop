using CandyShop.DTO;
using CandyShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.DAL;
using CandyShop.DAL.Models;
using CandyShop.DTO.Pastries;
using CandyShop.Filters;
using CandyShop.Interfaces;

namespace CandyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastriesController : ControllerBase
    {
        private readonly IPastriesService _pastriesService;

        public PastriesController(IPastriesService pastriesService)
        {
            _pastriesService = pastriesService;
        }

        

        [HttpPost]
        public async Task<object> AddPastry([FromBody] PastryInfo pastryInfo)
        {
            try
            {
                return await _pastriesService.AddPastry(pastryInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<object> GetPastries([FromQuery] QueryFilter filter)
        {
            
            return await _pastriesService.GetPastries(filter);
        }

        [HttpGet]
        [Route("{pastryId}")]
        public async Task<object> GetPastry([FromRoute] Guid pastryId)
        {
            try
            {
                return await _pastriesService.GetPastry(pastryId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("{pastryId}")]
        public async Task<object> UpdatePastry([FromRoute] Guid pastryId, [FromBody] PastryInfo pastryInfo)
        {
            try
            {
                return await _pastriesService.UpdatePastry(pastryId, pastryInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{pastryId}")]
        public async Task<object> DeletePastry([FromRoute] Guid pastryId)
        {
            try
            {
                return await _pastriesService.DeletePastry(pastryId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
