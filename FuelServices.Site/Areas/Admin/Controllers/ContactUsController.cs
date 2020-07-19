using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using FuelServices.Site.Helpers.Toast;
using System.Linq.Dynamic.Core;
using AutoMapper;
using FuelServices.DBContext.Domain;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : BaseController
    {
        
        public ContactUsController(AirportCoreContext context,IServiceProvider provider) : base(context, provider)
        {
        }

        // GET: Admin/ContactUs
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/ContactUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contactUs = await db.ContactUs
               .Include(c => c.Customer)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUs == null)
            {
                return NotFound();
            }
            try
            {
                contactUs.IsRead = true;
                db.Update(contactUs);

                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Message = Toast.ErrorToast(GetExceptionMessage(e));
            }
            return View(contactUs);
        }

        // GET: Admin/ContactUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUs = await db.ContactUs
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // POST: Admin/ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactUs = await db.ContactUs.FindAsync(id);
            db.ContactUs.Remove(contactUs);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult GetData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var contatUs = db.ContactUs;
                var tableData = contatUs.AsQueryable().Where(d => !d.IsDeleted);
                //(from temp in db.Offer select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(d => !d.IsDeleted).OrderByDescending(z => z.Created);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = AutoMapper.Mapper.Map<ContactUsViewModel>(data)
                });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private bool ContactUsExists(int id)
        {
            return db.ContactUs.Any(e => e.Id == id);
        }
    }
}
