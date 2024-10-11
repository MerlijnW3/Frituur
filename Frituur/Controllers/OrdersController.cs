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
    public class OrdersController : Controller
    {
        private readonly FrituurContext _context;

        public OrdersController(FrituurContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var frituurContext = _context.Order.Include(o => o.User);
            return View(await frituurContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var products = order.OrderProducts.Select(op => new ProductQuantity
            {
                ProductName = op.Product.Name,
                Quantity = op.Quantity,
                Price = (double)op.Product.Price,
                Discount = (double?)op.Product.Discount 
            }).ToList();

            var totalCost = products.Sum(p => (p.Price - (p.Discount ?? 0)) * p.Quantity);

            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                UserId = order.userId,
                OrderDate = order.OrderDate,
                Products = products,
                TotalCost = totalCost
            };

            return View(viewModel);
        }





        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Order";
            var viewModel = new OrderViewModel
            {
                Products = _context.Product.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList()
            };
            ViewData["userId"] = new SelectList(_context.User, "Id", "Id");
            return View(viewModel);
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    userId = viewModel.UserId,
                    OrderDate = DateTime.Now,
                    OrderProducts = new List<OrderProduct>()
                };

                for (int i = 0; i < viewModel.SelectedProductIds.Count; i++)
                {
                    if (viewModel.Quantities[i] > 0)
                    {
                        order.OrderProducts.Add(new OrderProduct
                        {
                            ProductId = viewModel.SelectedProductIds[i],
                            Quantity = viewModel.Quantities[i]
                        });
                    }
                }

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["userId"] = new SelectList(_context.User, "Id", "Id", viewModel.UserId);
            viewModel.Products = _context.Product.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
            return View(viewModel);
        }




        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit Order";
            ViewData["userId"] = new SelectList(_context.User, "Id", "Id", order.userId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,userId,OrderDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["userId"] = new SelectList(_context.User, "Id", "Id", order.userId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
