using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Entities;
using Services.Interfaces;

namespace Aviato.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly ICurrencyRepository _currency;

        public CurrenciesController(ICurrencyRepository currency)
        {
            _currency = currency;
        }

        // GET: Currencies
        public async Task<IActionResult> Index()
        {
            return View(await _currency.GetAllCurrencies());
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _currency.GetCurrency(id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _currency.AddCurrency(currency);
                await _currency.SaveCurrency();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _currency.FindCurrency(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name")] Currency currency)
        {
            if (id != currency.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _currency.UpdateCurrency(currency);
                    await _currency.SaveCurrency();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _currency.GetCurrency(id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var currency = await _currency.FindCurrency(id);
            _currency.RemoveCurrency(currency);
            await _currency.SaveCurrency();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(string id)
        {
            return _currency.Exists(id);
        }
    }
}
