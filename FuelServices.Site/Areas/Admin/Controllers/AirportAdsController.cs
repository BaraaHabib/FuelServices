using DBContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    public class AirportAdsController : BaseController
    {

        public AirportAdsController(AirportCoreContext context) : base(context)
        {
        }

        // GET: Admin/AirportAds
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

                var tableData = (from temp in db.AirportAds.Where(a => a.AdvertisementId == id)
                                 select new
                                 {
                                     itemOrder = temp.ItemOrder,
                                     id = temp.Id,
                                     airport_Name = temp.Airport.Name,
                                     range = temp.Range,
                                     isDeleted = temp.IsDeleted
                                 });

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(m => m.airport_Name == searchValue);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Admin/AirportAds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportAds = await db.AirportAds
                .Include(a => a.Advertisement)
                .Include(a => a.Airport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportAds == null)
            {
                return NotFound();
            }

            return View(airportAds);
        }

        // GET: Admin/AirportAds/Create
        public IActionResult Create(int id)
        {
            ViewData["AdvertisementId"] = id;
            ViewData["AirportId"] = new SelectList(db.Airport.Where(x => !x.IsDeleted), "Id", "Name");
            ViewData["CityId"] = new SelectList(db.City.Where(x => !x.IsDeleted), "Id", "Name");
            ViewData["CountryId"] = new SelectList(db.Country.Where(x => !x.IsDeleted), "Id", "Name");
            return View();
        }

        // POST: Admin/AirportAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AirportAds airportAds, IFormCollection pairs)
        {
            try
            {
                int selectedRange = 1;
                List<Airport> airports = new List<Airport>();
                if (pairs["Range"].ToString() == "1")
                {
                    selectedRange = 1;
                    int CountryId = int.Parse(pairs["CountryId"].ToString());
                    airports = db.Airport.Where(a => !a.IsDeleted && a.CountryId == CountryId).ToList();
                }
                if (pairs["Range"].ToString() == "2")
                {
                    selectedRange = 2;
                    int CityId = int.Parse(pairs["CityId"].ToString());
                    airports = db.Airport.Where(a => !a.IsDeleted && a.CityId == CityId).ToList();
                    if (airports == null || airports.Count() == 0)
                    {
                        City city = db.City.Find(CityId);
                        airports = db.Airport.Where(a => !a.IsDeleted && a.CountryId == city.CountryId).ToList();
                    }
                }
                if (pairs["Range"].ToString() == "3")
                {
                    selectedRange = 3;
                    string[] AirportsIds = pairs["AirportId"].ToString().Split(",");
                    foreach (var item in AirportsIds)
                    {
                        int id = int.Parse(item);
                        Airport airport = db.Airport.Find(id);
                        airports.Add(airport);
                    }
                }
                foreach (var item in airports)
                {
                    AirportAds airportAd = new AirportAds();
                    airportAd.AdvertisementId = airportAds.AdvertisementId;
                    airportAd.AirportId = item.Id;
                    airportAd.CaptionClicks = 0;
                    airportAd.Range = selectedRange;
                    db.AirportAds.Add(airportAd);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index), new { id = airportAds.AdvertisementId });
            }
            catch (Exception e)
            {
                ViewData["AdvertisementId"] = airportAds.AdvertisementId;
                ViewData["AirportId"] = new SelectList(db.Airport.Where(x => !x.IsDeleted), "Id", "Name", airportAds.AirportId);
                ViewData["CityId"] = new SelectList(db.City.Where(x => !x.IsDeleted), "Id", "Name", airportAds.Airport.CityId);
                ViewData["CountryId"] = new SelectList(db.Country.Where(x => !x.IsDeleted), "Id", "Name", airportAds.Airport.CountryId);

                return View(airportAds);
            }
        }

        // GET: Admin/AirportAds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportAds = await db.AirportAds.FindAsync(id);
            if (airportAds == null)
            {
                return NotFound();
            }
            ViewData["AdvertisementId"] = airportAds.AdvertisementId;
            ViewData["AirportId"] = new SelectList(db.Airport.Where(x => !x.IsDeleted), "Id", "Name", airportAds.AirportId);
            ViewData["CityId"] = new SelectList(db.City.Where(x => !x.IsDeleted), "Id", "Name", airportAds.Airport.CityId);
            ViewData["CountryId"] = new SelectList(db.Country.Where(x => !x.IsDeleted), "Id", "Name", airportAds.Airport.CountryId);
            return View(airportAds);
        }

        // POST: Admin/AirportAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertisementId,AirportId,Range,CaptionClicks,Id,Created,Modified,IsDeleted,ItemOrder")] AirportAds airportAds)
        {
            if (id != airportAds.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(airportAds);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportAdsExists(airportAds.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = airportAds.AdvertisementId });
            }
            ViewData["AdvertisementId"] = airportAds.AdvertisementId;
            ViewData["AirportId"] = new SelectList(db.Airport.Where(x => !x.IsDeleted), "Id", "Name", airportAds.AirportId);
            ViewData["CityId"] = new SelectList(db.City.Where(x => !x.IsDeleted), "Id", "Name", airportAds.Airport.CityId);
            ViewData["CountryId"] = new SelectList(db.Country.Where(x => !x.IsDeleted), "Id", "Name", airportAds.Airport.CountryId);
            return View(airportAds);
        }

        // GET: Admin/AirportAds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportAds = await db.AirportAds
                .Include(a => a.Advertisement)
                .Include(a => a.Airport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportAds == null)
            {
                return NotFound();
            }

            return View(airportAds);
        }

        // POST: Admin/AirportAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airportAds = await db.AirportAds.FindAsync(id);
            db.AirportAds.Remove(airportAds);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = airportAds.AdvertisementId });
        }

        private bool AirportAdsExists(int id)
        {
            return db.AirportAds.Any(e => e.Id == id);
        }
    }
}
