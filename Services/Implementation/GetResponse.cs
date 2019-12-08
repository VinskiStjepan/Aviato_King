using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class GetResponse : IGetResponse
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetResponse> _logger;
        private IToken _token;

        public GetResponse(IConfiguration configuration, ILogger<GetResponse> logger, IToken token)
        {
            _logger = logger;
            _token = token;
        }

        public async Task<string> GetHttpResponse(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    _logger.LogWarning($"HttpClient did not get response from get. {url}, {e.Message}, {e.InnerException}");
                    return "";
                }
            }
        }

        public async Task<string> GetHttpAuthResponse(string url)
        {
            string responseString;
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                if (_token.Access_token == null)
                {
                    string AutUrl = _configuration.GetValue<string>("Authentication:URL");
                    Dictionary<string, string> authenticationCredentials =
                        _configuration.GetSection("Authentication:Credentials").GetChildren()
                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                        .ToDictionary(x => x.Key, x => x.Value);

                    FormUrlEncodedContent content = new FormUrlEncodedContent(authenticationCredentials);
                    try
                    {
                        response = await client.PostAsync(AutUrl, content);
                        response.EnsureSuccessStatusCode();
                    }
                    catch (HttpRequestException e)
                    {
                        _logger.LogWarning($"HttpClient did not get response from post. {url}, {e.Message}, {e.InnerException}");
                        return "";
                    }

                    responseString = await response.Content.ReadAsStringAsync();

                    _token = JsonConvert.DeserializeObject<Token>(responseString);
                }

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token.Access_token);

                try
                {
                    response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    _logger.LogWarning($"HttpClient did not get response from get. {url}, {e.Message}, {e.InnerException}");
                    return "";
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
