
using AutoMapper;
using DBContext.Models;
using FuelServices.DBContext.Domain;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    public class AdvertisementCategoriesController : BaseController
    {

        public Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment { get; set; }

        public AdvertisementCategoriesController(AirportCoreContext context,
            IWebHostEnvironment hostingEnvironment) : base(context)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/AdvertisementCategories
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetData()
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

                var tableData = (from temp in db.AdvertisementCategory
                                 select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(m => m.Name == searchValue);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = GetService<IMapper>().Map<List<AdvertisementCategoryViewModel>>(data)
                });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Admin/AdvertisementCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementCategory = await db.AdvertisementCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisementCategory == null)
            {
                return NotFound();
            }

            return View(advertisementCategory);
        }

        // GET: Admin/AdvertisementCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdvertisementCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,Created,Modified,IsDeleted,ItemOrder")] AdvertisementCategory advertisementCategory)
        {
            if (ModelState.IsValid)
            {
                db.Add(advertisementCategory);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisementCategory);
        }

        // GET: Admin/AdvertisementCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementCategory = await db.AdvertisementCategory.FindAsync(id);
            if (advertisementCategory == null)
            {
                return NotFound();
            }
            return View(advertisementCategory);
        }

        // POST: Admin/AdvertisementCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Created,Modified,IsDeleted,ItemOrder")] AdvertisementCategory advertisementCategory)
        {
            if (id != advertisementCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(advertisementCategory);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementCategoryExists(advertisementCategory.Id))
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
            return View(advertisementCategory);
        }

        // GET: Admin/AdvertisementCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementCategory = await db.AdvertisementCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisementCategory == null)
            {
                return NotFound();
            }

            return View(advertisementCategory);
        }

        [HttpPost]
        public Response<bool> DeleteConfirmed(int? id)
        {
            try
            {
                var model = db.ContentManagement.Find(id);
                if (model == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                model.IsDeleted = true;
                var webPath = _hostingEnvironment.WebRootPath;
                var pathToDelete = webPath + model.ImageUrl;
                // TO DO: remove file
                /*
                 *
                 */

                db.ContentManagement.Remove(model);
                db.SaveChanges();
                Message = Toast.SucsessToast();
                return new Response<bool>(Constants.SUCCESS_CODE, true, Constants.SUCCESS);
            }
            catch (Exception exc)
            {
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(exc));
            }
        }

        private bool AdvertisementCategoryExists(int id)
        {
            return db.AdvertisementCategory.Any(e => e.Id == id);
        }
    }
}
