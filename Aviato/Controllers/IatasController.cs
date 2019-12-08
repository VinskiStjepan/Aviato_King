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
using Services.Implementation;

namespace Aviato.Controllers
{
    public class IatasController : Controller
    {
        private readonly IIataRepository _iata;
        private readonly IWikiService _wiki;

        public IatasController(IIataRepository iata, IWikiService wiki)
        {
            _iata = iata;
            _wiki = wiki;
        }

        // GET: Iatas
        public async Task<IActionResult> Index()
        {
            return View(await _iata.GetAllIatas());
        }

        // GET: Iatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iata = await _iata.GetIata(id);
            if (iata == null)
            {
                return NotFound();
            }

            return View(iata);
        }

        // GET: Iatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Iatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name")] Iata iata)
        {
            if (ModelState.IsValid)
            {
                _iata.AddIata(iata);
                await _iata.SaveIata();
                return RedirectToAction(nameof(Index));
            }
            return View(iata);
        }

        // GET: Iatas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iata = await _iata.FindIata(id);
            if (iata == null)
            {
                return NotFound();
            }
            return View(iata);
        }

        // POST: Iatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name")] Iata iata)
        {
            if (id != iata.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _iata.UpdateIata(iata);
                    await _iata.SaveIata();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IataExists(iata.Code))
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
            return View(iata);
        }

        // GET: Iatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iata = await _iata.GetIata(id);
            if (iata == null)
            {
                return NotFound();
            }

            return View(iata);
        }

        // POST: Iatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var iata = await _iata.FindIata(id);
            _iata.RemoveIata(iata);
            await _iata.SaveIata();
            return RedirectToAction(nameof(Index));
        }

        private bool IataExists(string id)
        {
            return _iata.Exists(id);
        }

        public async Task<IActionResult> Import()
        {
            List<Iata> iatas = new List<Iata>();
            iatas = await _wiki.Import();

            foreach(Iata iata in iatas)
            {
                _iata.AddIata(iata);
                await _iata.SaveIata();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
