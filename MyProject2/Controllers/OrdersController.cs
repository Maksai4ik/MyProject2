using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyProject2.Models;
using MyProject2.Data;
using MyProject2.Data.Migrations;
using MyProject2.Models;

namespace MyProject2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string SearchString, int? DriverID)
        {
            var drivers = _context.Driver.Select(b => b.Id);
            var orders = _context.Order.Include(o => o.Car).Include(o => o.Driver).AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                orders = orders.Where(s => s.NameFirm!.ToUpper().Contains(SearchString.ToUpper()));
            }

            //if ((DriverID != null))
            //{
            //    orders = orders.Where(x => x.Id == DriverID);
            //}

            if (DriverID.HasValue)
            {
                orders = orders.Where(o => o.Driver != null && o.Driver.Id == DriverID.Value);
            }

            var sql = orders.ToQueryString();
            var orderdriverVM = new OrderDriverViewModel
            {
                Drivers = new SelectList(await _context.Driver.Distinct().ToListAsync(), "Id", "DiverName"),
                Orders = await orders.ToListAsync()
            };
            return View(orderdriverVM);
        }

      


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Car)
                .Include(o => o.Driver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CarID"] = new SelectList(_context.Car, "Id", "NameCar");
            ViewData["DriverID"] = new SelectList(_context.Driver, "Id", "DiverName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameFirm,OrderNumber,Price,OrderDate,CarID,DriverID")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarID"] = new SelectList(_context.Car, "Id", "NameCar", order.CarID);
            ViewData["DriverID"] = new SelectList(_context.Driver, "Id", "DiverName", order.DriverID);
            return View(order);
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
            ViewData["CarID"] = new SelectList(_context.Car, "Id", "NameCar", order.CarID);
            ViewData["DriverID"] = new SelectList(_context.Driver, "Id", "DiverName", order.DriverID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameFirm,OrderNumber,Price,OrderDate,CarID,DriverID")] Order order)
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
            ViewData["CarID"] = new SelectList(_context.Car, "Id", "NameCar", order.CarID);
            ViewData["DriverID"] = new SelectList(_context.Driver, "Id", "DiverName", order.DriverID);
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
                .Include(o => o.Car)
                .Include(o => o.Driver)
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
