using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class IataRepository : IIataRepository
    {
        private readonly AppDbContext _context;

        public IataRepository(AppDbContext Context)
        {
            _context = Context;
        }

        public void AddIata(Iata iata)
        {
            _context.Add(iata);
        }

        public bool Exists(string id)
        {
            return _context.Iatas.Any(c => c.Code == id);
        }

        public async Task<Iata> FindIata(string id)
        {
            return await _context.Iatas.FindAsync(id);
        }

        public async Task<List<Iata>> GetAllIatas()
        {
            return await _context.Iatas
                            .OrderBy(c => c.Code)
                            .ToListAsync();
        }

        public async Task<Iata> GetIata(string id)
        {
            return await _context.Iatas
                            .FirstOrDefaultAsync(c => c.Code == id);
        }

        public void RemoveIata(Iata iata)
        {
            _context.Remove(iata);
        }

        public async Task<int> SaveIata()
        {
            return await _context.SaveChangesAsync();
        }

        public void UpdateIata(Iata iata)
        {
            _context.Update(iata);
        }
    }
}
