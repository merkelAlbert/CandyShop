using System;
using System.Collections.Generic;
using System.Text;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;

namespace ConsoleCandyShop.Controllers
{
    public class PastriesController
    {
        private IPastriesService _pastryService;

        public PastriesController(IPastriesService pastryService)
        {
            _pastryService = pastryService;
        }

        public void AddPastry(Pastry pastry)
        {
            try
            {
                _pastryService.AddPastry(pastry);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при создании кондитерского изделия: {e.Message}");
            }
        }

        public Pastry GetPastry(int id)
        {
            try
            {
                return _pastryService.GetPastry(id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при получении кондитерского изделия: {e.Message}");
                return null;
            }
        }

        public List<Pastry> GetPastries()
        {
            return _pastryService.GetPastries();
        }

        public void UpdatePastry(int id, Pastry pastry)
        {
            try
            {
                _pastryService.UpdatePastry(id, pastry);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при изменении кондитерского изделия: {e.Message}");
            }
        }

        public void DeletePastry(int id)
        {
            try
            {
                _pastryService.DeletePastry(id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Ошибка при удалении кондитерского изделия: {e.Message}");
            }
        }
    }
}
