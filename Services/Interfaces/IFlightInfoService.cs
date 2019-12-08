using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFlightInfoService
    {
        Task<SearchModel> ShowData(SearchModel model);
    }
}
