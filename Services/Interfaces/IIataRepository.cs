using Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIataRepository
    {
        Task<List<Iata>> GetAllIatas();
        Task<Iata> GetIata(string id);
        void AddIata(Iata iata);
        Task<int> SaveIata();
        Task<Iata> FindIata(string id);
        void UpdateIata(Iata iata);
        void RemoveIata(Iata iata);
        bool Exists(string id);
    }
}
