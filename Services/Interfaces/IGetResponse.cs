using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGetResponse
    {
        Task<string> GetHttpResponse(string url);
        Task<string> GetHttpAuthResponse(string url);
    }
}
