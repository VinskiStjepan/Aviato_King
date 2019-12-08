using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class FlightInfoService : IFlightInfoService
    {

        private readonly IConfiguration _configuration;
        private readonly IGetResponse _response;
        private IMemoryCache _cache;

        public FlightInfoService(IConfiguration configuration, IGetResponse response, IMemoryCache cache)
        {
            _configuration = configuration;
            _response = response;
            _cache = cache;
        }

        public async Task<SearchModel> ShowData(SearchModel model)
        {
            model.LocalStoragekey = model.OriginCode + model.DestinationCode + model.DepartureDate
                                     + model.ReturnDate + model.Passengers + model.CurrencyCode;

            if (!_cache.TryGetValue(model.LocalStoragekey, out string responseString))
            {
                string amdUrl = _configuration.GetValue<string>("AmadeusFlightffersUrl");

                amdUrl += $"?origin={model.OriginCode}&destination={model.DestinationCode}&departureDate={model.DepartureDate}" +
                        $"&returnDate={model.ReturnDate}&adults={model.Passengers}&currency={model.CurrencyCode}";

                responseString = await _response.GetHttpAuthResponse(amdUrl);

                if (responseString != "")
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    _cache.Set(model.LocalStoragekey, responseString, cacheEntryOptions);
                }
            }

            if (responseString != "")
            {
                AmadeusJson amadeus = JsonConvert.DeserializeObject<AmadeusJson>(responseString);

                model.Offers = amadeus.GetAllOffers();
            }
            return model;
        }
    }
}
