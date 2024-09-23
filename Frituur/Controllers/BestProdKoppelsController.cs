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
    public class BestProdKoppelsController : Controller
    {
        private readonly FrituurContext _context;

        public BestProdKoppelsController(FrituurContext context)
        {
            _context = context;
        }

        // GET: BestProdKoppels
        public async Task<IActionResult> Index()
        {
            return View(await _context.BestProdKoppel.ToListAsync());
        }

        // GET: BestProdKoppels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestProdKoppel = await _context.BestProdKoppel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestProdKoppel == null)
            {
                return NotFound();
            }

            return View(bestProdKoppel);
        }

        // GET: BestProdKoppels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BestProdKoppels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BestellingId,ProductId")] BestProdKoppel bestProdKoppel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestProdKoppel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bestProdKoppel);
        }

        // GET: BestProdKoppels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestProdKoppel = await _context.BestProdKoppel.FindAsync(id);
            if (bestProdKoppel == null)
            {
                return NotFound();
            }
            return View(bestProdKoppel);
        }

        // POST: BestProdKoppels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BestellingId,ProductId")] BestProdKoppel bestProdKoppel)
        {
            if (id != bestProdKoppel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestProdKoppel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestProdKoppelExists(bestProdKoppel.Id))
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
            return View(bestProdKoppel);
        }

        // GET: BestProdKoppels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestProdKoppel = await _context.BestProdKoppel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestProdKoppel == null)
            {
                return NotFound();
            }

            return View(bestProdKoppel);
        }

        // POST: BestProdKoppels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestProdKoppel = await _context.BestProdKoppel.FindAsync(id);
            if (bestProdKoppel != null)
            {
                _context.BestProdKoppel.Remove(bestProdKoppel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestProdKoppelExists(int id)
        {
            return _context.BestProdKoppel.Any(e => e.Id == id);
        }
    }
}
