using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Models;

namespace CMS.Controllers
{
    public class MeterController : Controller
    {
        private readonly NeondbContext _context;

        public MeterController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Meter
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Meters.Include(m => m.Customer);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Meter/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _context.Meters
                .Include(m => m.Customer)
                .FirstOrDefaultAsync(m => m.Meterid == id);
            if (meter == null)
            {
                return NotFound();
            }

            return View(meter);
        }

        // GET: Meter/Create
        public IActionResult Create()
        {
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid");
            return View();
        }

        // POST: Meter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Meterid,Customerid,Meternumber,Metertype")] Meter meter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", meter.Customerid);
            return View(meter);
        }

        // GET: Meter/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _context.Meters.FindAsync(id);
            if (meter == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", meter.Customerid);
            return View(meter);
        }

        // POST: Meter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Meterid,Customerid,Meternumber,Metertype")] Meter meter)
        {
            if (id != meter.Meterid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeterExists(meter.Meterid))
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
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", meter.Customerid);
            return View(meter);
        }

        // GET: Meter/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _context.Meters
                .Include(m => m.Customer)
                .FirstOrDefaultAsync(m => m.Meterid == id);
            if (meter == null)
            {
                return NotFound();
            }

            return View(meter);
        }

        // POST: Meter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var meter = await _context.Meters.FindAsync(id);
            if (meter != null)
            {
                _context.Meters.Remove(meter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeterExists(string id)
        {
            return _context.Meters.Any(e => e.Meterid == id);
        }
    }
}
