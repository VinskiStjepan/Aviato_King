using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aviato.Models;
using Services.Implementation;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Aviato.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightInfoService _flight;
        private readonly ICurrencyRepository _currRep;
        private readonly IIataRepository _iataRep;

        public HomeController(IFlightInfoService flight, ICurrencyRepository currRep, IIataRepository iataRep)
        {
            _flight = flight;
            _currRep = currRep;
            _iataRep = iataRep;
        }

        public async Task<IActionResult> Index()
        {
            SearchModel model = new SearchModel()
            {
                Currencies = await _currRep.GetAllCurrencies(),
                IataCodes = await _iataRep.GetAllIatas()
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShowData(SearchModel model)
        {
            await _flight.ShowData(model);
            if (model.Offers == null) { return RedirectToAction("Error"); }
            return View(model);
        }
    }
}
