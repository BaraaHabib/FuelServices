using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using Microsoft.AspNetCore.Identity;
using FuelServices.Site.Helpers.Toast;
using Site.Helpers;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FuelSuppliersController : BaseController
    {

        public FuelSuppliersController(AirportCoreContext context,IServiceProvider serviceProvider):base(context,serviceProvider)
        {
        }

        // GET: Admin/FuelSuppliers
        public async Task<IActionResult> Index()
        {

            var airportCoreContext = db.FuelSupplier.Include(f => f.Country).Include(f => f.User);
            return View(await airportCoreContext.ToListAsync());
        }

        // GET: Admin/FuelSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelSupplier = await db.FuelSupplier
                .Include(f => f.Country)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuelSupplier == null)
            {
                return NotFound();
            }

            return View(fuelSupplier);
        }

        // GET: Admin/FuelSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelSupplier = await db.FuelSupplier
                .Include(f => f.Country)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuelSupplier == null)
            {
                return NotFound();
            }


            return View(fuelSupplier);
        }

        // POST: Admin/FuelSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuelSupplier = await db.FuelSupplier.FindAsync(id);

            try
            {
                fuelSupplier.IsDeleted = true;
                db.Update(fuelSupplier);
                await db.SaveChangesAsync();
                var user = await UserManager.FindByIdAsync(fuelSupplier.UserId);
                await UserManager.SetLockoutEnabledAsync(user, true);
                Message = Toast.SucsessToast();

            }
            catch (Exception e)
            {
                Message = new Toast(GetExceptionMessage(e),ToasterType.error);
                return View(fuelSupplier);

            }

            return RedirectToAction(nameof(Index));
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Response<bool>> ToggleActivate(int? id)
        {
            try
            {

                var model = db.FuelSupplier.Find(id);

                if (model == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);

                var user = model.User;

                if(user.Id == (await GetCurrentUserIdAsync()))
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
                Serilog.Log.Error(exc,$"controller: FuelSuppliers",$"action : ToggleActivate");
                Message = Toast.ErrorToast();
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(exc));
            }
        }


        private bool FuelSupplierExists(int id)
        {
            return db.FuelSupplier.Any(e => e.Id == id);
        }
    }
}
