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
    public class BillController : Controller
    {
        private readonly NeondbContext _context;

        public BillController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Bills.Include(b => b.Reading);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Reading)
                .FirstOrDefaultAsync(m => m.Billid == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bill/Create
        public IActionResult Create()
        {
            ViewData["Readingid"] = new SelectList(_context.Readings, "Readingid", "Readingid");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Billid,Readingid,Amount,Duedate,Status")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Readingid"] = new SelectList(_context.Readings, "Readingid", "Readingid", bill.Readingid);
            return View(bill);
        }

        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["Readingid"] = new SelectList(_context.Readings, "Readingid", "Readingid", bill.Readingid);
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Billid,Readingid,Amount,Duedate,Status")] Bill bill)
        {
            if (id != bill.Billid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Billid))
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
            ViewData["Readingid"] = new SelectList(_context.Readings, "Readingid", "Readingid", bill.Readingid);
            return View(bill);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Reading)
                .FirstOrDefaultAsync(m => m.Billid == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(string id)
        {
            return _context.Bills.Any(e => e.Billid == id);
        }
    }
}
