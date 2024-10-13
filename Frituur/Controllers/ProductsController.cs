using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frituur.Data;
using Frituur.Models;

namespace Frituur.Controllers
{
    public class ProductsController : Controller
    {
        private readonly FrituurContext _context;

        public ProductsController(FrituurContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Discount")] Product product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                // Ensure Price and Discount are correctly parsed
                product.Price = ParseDouble(Request.Form["Price"]);
                product.Discount = ParseDouble(Request.Form["Discount"]);

                if (photo != null && photo.Length > 0)
                {
                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);
                        product.Photo = PhotoConverter.ConvertToBase64String(memoryStream.ToArray());
                    }
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        private double? ParseDouble(string value)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            return null;
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Discount")] Product product, IFormFile photo)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ensure Price and Discount are correctly parsed
                    product.Price = ParseDouble(Request.Form["Price"]);
                    product.Discount = ParseDouble(Request.Form["Discount"]);

                    if (photo != null && photo.Length > 0)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            await photo.CopyToAsync(memoryStream);
                            product.Photo = PhotoConverter.ConvertToBase64String(memoryStream.ToArray());
                        }
                    }
                    else
                    {
                        // Preserve existing photo if no new photo is uploaded
                        var existingProduct = await _context.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                        product.Photo = existingProduct?.Photo;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
