using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierContactsController : Controller
    {
        private readonly AirportCoreContext _context;

        public SupplierContactsController(AirportCoreContext context)
        {
            _context = context;
        }

        // GET: Admin/SupplierContacts
        public async Task<IActionResult> Index()
        {
            var airportCoreContext = _context.SupplierContact.Include(s => s.Contact).Include(s => s.FuelSupplier);
            return View(await airportCoreContext.ToListAsync());
        }

        // GET: Admin/SupplierContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierContact = await _context.SupplierContact
                .Include(s => s.Contact)
                .Include(s => s.FuelSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierContact == null)
            {
                return NotFound();
            }

            return View(supplierContact);
        }

        // GET: Admin/SupplierContacts/Create
        public IActionResult Create()
        {
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "DisplayName");
            ViewData["FuelSupplierId"] = new SelectList(_context.FuelSupplier, "Id", "Name");
            return View();
        }

        // POST: Admin/SupplierContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuelSupplierId,ContactId,Value,Id,Created,Modified,IsDeleted,ItemOrder")] SupplierContact supplierContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "DisplayName", supplierContact.ContactId);
            ViewData["FuelSupplierId"] = new SelectList(_context.FuelSupplier, "Id", "Name", supplierContact.FuelSupplierId);
            return View(supplierContact);
        }

        // GET: Admin/SupplierContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierContact = await _context.SupplierContact.FindAsync(id);
            if (supplierContact == null)
            {
                return NotFound();
            }
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "DisplayName", supplierContact.ContactId);
            ViewData["FuelSupplierId"] = new SelectList(_context.FuelSupplier, "Id", "Name", supplierContact.FuelSupplierId);
            return View(supplierContact);
        }

        // POST: Admin/SupplierContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuelSupplierId,ContactId,Value,Id,Created,Modified,IsDeleted,ItemOrder")] SupplierContact supplierContact)
        {
            if (id != supplierContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierContactExists(supplierContact.Id))
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
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "DisplayName", supplierContact.ContactId);
            ViewData["FuelSupplierId"] = new SelectList(_context.FuelSupplier, "Id", "Name", supplierContact.FuelSupplierId);
            return View(supplierContact);
        }

        // GET: Admin/SupplierContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierContact = await _context.SupplierContact
                .Include(s => s.Contact)
                .Include(s => s.FuelSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierContact == null)
            {
                return NotFound();
            }

            return View(supplierContact);
        }

        // POST: Admin/SupplierContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplierContact = await _context.SupplierContact.FindAsync(id);
            _context.SupplierContact.Remove(supplierContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierContactExists(int id)
        {
            return _context.SupplierContact.Any(e => e.Id == id);
        }
    }
}
