using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public static class AviatoServiceCollectionExtension
    {
        public static IServiceCollection AddAviatoServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IIataRepository, IataRepository>();
            services.AddScoped<IWikiService, WikiService>();
            services.AddScoped<IFlightInfoService, FlightInfoService>();
            services.AddSingleton<IGetResponse, GetResponse>();
            services.AddSingleton<IToken, Token>();

            return services;
        }
    }
}
