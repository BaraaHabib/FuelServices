using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    public class AdvertisementPropertiesController : BaseController
    {
        public AdvertisementPropertiesController(AirportCoreContext context) : base(context)
        {
        }

        // GET: Admin/AdvertisementProperties
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public IActionResult GetData(int id)
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

                var tableData = (from temp in db.AdvertisementProperty.Where(a => a.AdvertisementId == id)
                                 select new
                                 {
                                     itemOrder = temp.ItemOrder,
                                     id = temp.Id,
                                     property_Name = temp.AdvertisementTypeProperty.Name,
                                     value = temp.Value,
                                     isDeleted = temp.IsDeleted,
                                 });

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(m => m.property_Name == searchValue);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = data
                });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Admin/AdvertisementProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementProperty = await db.AdvertisementProperty
                .Include(a => a.Advertisement)
                .Include(a => a.AdvertisementTypeProperty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisementProperty == null)
            {
                return NotFound();
            }

            return View(advertisementProperty);
        }

        // GET: Admin/AdvertisementProperties/Create
        public IActionResult Create(int id)
        {
            ViewData["AdvertisementId"] = id;
            ViewData["AdvertisementTypePropertyId"] = new SelectList(db.AdvertisementTypeProperty.Where(x => !x.IsDeleted), "Id", "DisplayName");
            return View();
        }

        // POST: Admin/AdvertisementProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertisementId,AdvertisementTypePropertyId,Value,Unit,Id,Created,Modified,IsDeleted,ItemOrder")] AdvertisementProperty advertisementProperty)
        {
            if (ModelState.IsValid)
            {
                advertisementProperty.Id = 0;
                db.Add(advertisementProperty);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = advertisementProperty.AdvertisementId });
            }
            ViewData["AdvertisementId"] = advertisementProperty.AdvertisementId;
            ViewData["AdvertisementTypePropertyId"] = new SelectList(db.AdvertisementTypeProperty.Where(x => !x.IsDeleted), "Id", "DisplayName", advertisementProperty.AdvertisementTypePropertyId);
            return View(advertisementProperty);
        }

        // GET: Admin/AdvertisementProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementProperty = await db.AdvertisementProperty.FindAsync(id);
            if (advertisementProperty == null)
            {
                return NotFound();
            }
            ViewData["AdvertisementId"] = new SelectList(db.Advertisement, "Id", "AnchorUrl", advertisementProperty.AdvertisementId);
            ViewData["AdvertisementTypePropertyId"] = new SelectList(db.AdvertisementTypeProperty.Where(x => !x.IsDeleted), "Id", "DisplayName", advertisementProperty.AdvertisementTypePropertyId);
            return View(advertisementProperty);
        }

        // POST: Admin/AdvertisementProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertisementId,AdvertisementTypePropertyId,Value,Unit,Id,Created,Modified,IsDeleted,ItemOrder")] AdvertisementProperty advertisementProperty)
        {
            if (id != advertisementProperty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(advertisementProperty);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementPropertyExists(advertisementProperty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = advertisementProperty.AdvertisementId });
            }
            ViewData["AdvertisementId"] = advertisementProperty.AdvertisementId;
            ViewData["AdvertisementTypePropertyId"] = new SelectList(db.AdvertisementTypeProperty.Where(x => !x.IsDeleted), "Id", "DisplayName", advertisementProperty.AdvertisementTypePropertyId);
            return View(advertisementProperty);
        }

        // GET: Admin/AdvertisementProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementProperty = await db.AdvertisementProperty
                .Include(a => a.Advertisement)
                .Include(a => a.AdvertisementTypeProperty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisementProperty == null)
            {
                return NotFound();
            }

            return View(advertisementProperty);
        }

        // POST: Admin/AdvertisementProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisementProperty = await db.AdvertisementProperty.FindAsync(id);
            db.AdvertisementProperty.Remove(advertisementProperty);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = advertisementProperty.AdvertisementId });
        }

        private bool AdvertisementPropertyExists(int id)
        {
            return db.AdvertisementProperty.Any(e => e.Id == id);
        }
    }
}
