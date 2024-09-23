using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Frituur.Data;
using Frituur.Models;

namespace Frituur.Controllers
{
    public class BestellingsController : Controller
    {
        private readonly FrituurContext _context;

        public BestellingsController(FrituurContext context)
        {
            _context = context;
        }

        // GET: Bestellings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bestelling.ToListAsync());
        }

        // GET: Bestellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // GET: Bestellings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bestellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GebruikerId")] Bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bestelling);
        }

        // GET: Bestellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling.FindAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }
            return View(bestelling);
        }

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GebruikerId")] Bestelling bestelling)
        {
            if (id != bestelling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(bestelling.Id))
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
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestelling.FindAsync(id);
            if (bestelling != null)
            {
                _context.Bestelling.Remove(bestelling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestelling.Any(e => e.Id == id);
        }
    }
}
