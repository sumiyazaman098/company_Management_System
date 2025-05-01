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
    public class ReadingController : Controller
    {
        private readonly NeondbContext _context;

        public ReadingController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Reading
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Readings.Include(r => r.Meter);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Reading/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .Include(r => r.Meter)
                .FirstOrDefaultAsync(m => m.Readingid == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // GET: Reading/Create
        public IActionResult Create()
        {
            ViewData["Meterid"] = new SelectList(_context.Meters, "Meterid", "Meterid");
            return View();
        }

        // POST: Reading/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Readingid,Meterid,Date,Unitsconsumed")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Meterid"] = new SelectList(_context.Meters, "Meterid", "Meterid", reading.Meterid);
            return View(reading);
        }

        // GET: Reading/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }
            ViewData["Meterid"] = new SelectList(_context.Meters, "Meterid", "Meterid", reading.Meterid);
            return View(reading);
        }

        // POST: Reading/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Readingid,Meterid,Date,Unitsconsumed")] Reading reading)
        {
            if (id != reading.Readingid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadingExists(reading.Readingid))
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
            ViewData["Meterid"] = new SelectList(_context.Meters, "Meterid", "Meterid", reading.Meterid);
            return View(reading);
        }

        // GET: Reading/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .Include(r => r.Meter)
                .FirstOrDefaultAsync(m => m.Readingid == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // POST: Reading/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var reading = await _context.Readings.FindAsync(id);
            if (reading != null)
            {
                _context.Readings.Remove(reading);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadingExists(string id)
        {
            return _context.Readings.Any(e => e.Readingid == id);
        }
    }
}
