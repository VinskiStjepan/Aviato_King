using Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAllCurrencies();
        Task<Currency> GetCurrency(string id);
        void AddCurrency(Currency currency);
        Task<int> SaveCurrency();
        Task<Currency> FindCurrency(string id);
        void UpdateCurrency(Currency currency);
        void RemoveCurrency(Currency currency);
        bool Exists(string id);
    }
}
