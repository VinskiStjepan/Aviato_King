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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;

        public CurrencyRepository(AppDbContext Context)
        {
            _context = Context;
        }

        public async Task<List<Currency>> GetAllCurrencies()
        {
            return await _context.Currencies
                            .OrderBy(c => c.Code)
                            .ToListAsync();
        }

        public async Task<Currency> GetCurrency(string id)
        {
            return await _context.Currencies
                            .FirstOrDefaultAsync(c => c.Code == id);
        }

        public void AddCurrency(Currency currency)
        {
            _context.Add(currency);
        }

        public async Task<int> SaveCurrency()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Currency> FindCurrency(string id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public void UpdateCurrency(Currency currency)
        {
            _context.Update(currency);
        }

        public void RemoveCurrency(Currency currency)
        {
            _context.Remove(currency);
        }

        public bool Exists(string id)
        {
            return _context.Currencies.Any(c => c.Code == id);
        }
    }
}
