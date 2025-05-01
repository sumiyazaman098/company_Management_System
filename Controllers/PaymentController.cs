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
    public class PaymentController : Controller
    {
        private readonly NeondbContext _context;

        public PaymentController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Payments.Include(p => p.Bill);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Bill)
                .FirstOrDefaultAsync(m => m.Paymentid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            ViewData["Billid"] = new SelectList(_context.Bills, "Billid", "Billid");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Paymentid,Billid,Paymentdate,Amountpaid,Paymentmethod")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Billid"] = new SelectList(_context.Bills, "Billid", "Billid", payment.Billid);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["Billid"] = new SelectList(_context.Bills, "Billid", "Billid", payment.Billid);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Paymentid,Billid,Paymentdate,Amountpaid,Paymentmethod")] Payment payment)
        {
            if (id != payment.Paymentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Paymentid))
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
            ViewData["Billid"] = new SelectList(_context.Bills, "Billid", "Billid", payment.Billid);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Bill)
                .FirstOrDefaultAsync(m => m.Paymentid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(string id)
        {
            return _context.Payments.Any(e => e.Paymentid == id);
        }
    }
}
