using Database.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IWikiService
    {
        Task<List<Iata>> Import();
    }
}
