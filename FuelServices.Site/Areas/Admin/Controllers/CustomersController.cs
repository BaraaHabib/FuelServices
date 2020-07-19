using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using Site.Helpers;
using FuelServices.Site.Helpers.Toast;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : BaseController
    {

        public CustomersController(AirportCoreContext context, IServiceProvider provider) : base(context,provider)
        {
        }

        // GET: Admin/Customers
        public async Task<IActionResult> Index()
        {
            var airportCoreContext = db.Customer.Include(c => c.Country).Include(c => c.User);
            return View(await airportCoreContext.ToListAsync());
        }

        // GET: Admin/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await db.Customer
                .Include(c => c.Country)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customers/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(db.Country, "Id", "Name");
            ViewData["UserId"] = new SelectList(db.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,ImageUrl,CountryId,Id,Created,Modified,IsDeleted,ItemOrder")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(db.Country, "Id", "Name", customer.CountryId);
            ViewData["UserId"] = new SelectList(db.Users, "Id", "Id", customer.UserId);
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await db.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(db.Country, "Id", "Name", customer.CountryId);
            ViewData["UserId"] = new SelectList(db.Users, "Id", "Id", customer.UserId);
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,ImageUrl,CountryId,Id,Created,Modified,IsDeleted,ItemOrder")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(customer);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["CountryId"] = new SelectList(db.Country, "Id", "Name", customer.CountryId);
            ViewData["UserId"] = new SelectList(db.Users, "Id", "Id", customer.UserId);
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await db.Customer
                .Include(c => c.Country)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await db.Customer.FindAsync(id);
            db.Customer.Remove(customer);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Response<bool>> ToggleActivate(int? id)
        {
            try
            {

                var model = db.Customer.Find(id);

                if (model == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);

                var user = model.User;

                if (user.Id == (await GetCurrentUserIdAsync()))
                {
                    return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, Constants.SOMETHING_WRONG);
                }

                user.IsActive = !user.IsActive;
                db.Update(user);
                db.SaveChanges();

                Message = Toast.SucsessToast();
                return new Response<bool>(Constants.SUCCESS_CODE, true, Constants.SUCCESS);
            }
            catch (Exception exc)
            {
                Serilog.Log.Error(exc, $"controller: Customers", $"action : ToggleActivate");
                Message = Toast.ErrorToast(); return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(exc));
            }
        }

        private bool CustomerExists(int id)
        {
            return db.Customer.Any(e => e.Id == id);
        }
    }
}
