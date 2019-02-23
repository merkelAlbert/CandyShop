using System;
using System.Collections.Generic;
using System.Text;
using ConsoleCandyShop.DAL;

namespace ConsoleCandyShop.Interfaces
{
    public interface IPastriesService
    {
        Pastry GetPastry(int pastryId);
        List<Pastry> GetPastries();
        void AddPastry(Pastry pastry);
        void UpdatePastry(int pastryId, Pastry pastry);
        void DeletePastry(int pastryId);
    }
}
