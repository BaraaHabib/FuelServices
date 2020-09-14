using AutoMapper;
using DBContext.Models;
using FuelServices.DBContext.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    public class AdvertisementsController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdvertisementsController(AirportCoreContext context, IWebHostEnvironment hostingEnvironment) : base(context)
        {
            db = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Advertisements
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

                var tableData = (from temp in db.Advertisement.Where(x => x.Status == "Piblished"
                                 || x.Status == "Pending")
                                 select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(m => m.Title == searchValue);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = GetService<IMapper>().Map<List<AdvertisementViewModel>>(data)
                });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult RequestedIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetWaitingData()
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

                var tableData = (from temp in db.Advertisement.Where(x => !x.IsDeleted
                                 && x.Status == "Requested")
                                 select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(m => m.Title == searchValue);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = GetService<IMapper>().Map<List<AdvertisementViewModel>>(data)
                });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult ArchivedIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetArchivedData()
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

                var tableData = (from temp in db.Advertisement.Where(x => x.IsDeleted
                                 || x.Status == "Rejected" || x.Status == "Archived")
                                 select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(m => m.Title == searchValue);
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = GetService<IMapper>().Map<List<AdvertisementViewModel>>(data)
                });
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Admin/Advertisements/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await db.Advertisement.FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Admin/Advertisements/Create
        public IActionResult Create()
        {
            ViewData["AdvertisementCategoryId"] = new SelectList(db.AdvertisementCategory.Where(x => !x.IsDeleted), "Id", "Name");
            ViewData["AdvertisementImageTypeId"] = new SelectList(db.AdvertisementImageTypes.Where(x => !x.IsDeleted), "Id", "Name");
            ViewData["AdvertisementOwnerId"] = new SelectList(db.AdvertisementOwner.Where(x => !x.IsDeleted), "Id", "Email");
            ViewData["AdvertisementTypeId"] = new SelectList(db.AdvertisementType.Where(x => !x.IsDeleted), "Id", "DisplayName");
            return View();
        }

        // POST: Admin/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advertisement advertisement, IFormCollection formCollection)
        {
            try
            {
                if (advertisement.file != null)
                {
                    FileInfo fi = new FileInfo(advertisement.file.FileName);
                    var newFilename = "P" + advertisement.Id + "_" + string.Format("{0:d}",
                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    var webPath = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\images\ads\" + newFilename);
                    var pathToSave = @"/images/ads/" + newFilename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        advertisement.file.CopyTo(stream);
                    }
                    advertisement.ImageUrl = pathToSave;
                }
                var offer_StartDate = DateTime.Parse(formCollection["offer_StartDate"]);
                var offer_EndDate = DateTime.Parse(formCollection["offer_EndDate"]);
                advertisement.PublishDate = offer_StartDate;
                advertisement.EndDate = offer_EndDate;
                advertisement.CaptionClicks = 0;
                advertisement.Status = "Accepted";
                advertisement.AdvertisementOwner = null;
                db.Add(advertisement);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewData["AdvertisementCategoryId"] = new SelectList(db.AdvertisementCategory.Where(x => !x.IsDeleted), "Id", "Name", advertisement.AdvertisementCategoryId);
                ViewData["AdvertisementImageTypeId"] = new SelectList(db.AdvertisementImageTypes.Where(x => !x.IsDeleted), "Id", "Name", advertisement.AdvertisementImageTypeId);
                ViewData["AdvertisementOwnerId"] = new SelectList(db.AdvertisementOwner.Where(x => !x.IsDeleted), "Id", "Email", advertisement.AdvertisementOwnerId);
                ViewData["AdvertisementTypeId"] = new SelectList(db.AdvertisementType.Where(x => !x.IsDeleted), "Id", "DisplayName", advertisement.AdvertisementTypeId);
                return View(advertisement);
            }
        }

        // GET: Admin/Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await db.Advertisement.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            ViewData["AdvertisementCategoryId"] = new SelectList(db.AdvertisementCategory.Where(x => !x.IsDeleted), "Id", "Name", advertisement.AdvertisementCategoryId);
            ViewData["AdvertisementImageTypeId"] = new SelectList(db.AdvertisementImageTypes.Where(x => !x.IsDeleted), "Id", "Name", advertisement.AdvertisementImageTypeId);
            ViewData["AdvertisementOwnerId"] = new SelectList(db.AdvertisementOwner.Where(x => !x.IsDeleted), "Id", "Email", advertisement.AdvertisementOwnerId);
            ViewData["AdvertisementTypeId"] = new SelectList(db.AdvertisementType.Where(x => !x.IsDeleted), "Id", "DisplayName", advertisement.AdvertisementTypeId);
            return View(advertisement);
        }

        // POST: Admin/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Advertisement advertisement, IFormCollection formCollection)
        {
            try
            {
                if (id != advertisement.Id)
                {
                    return NotFound();
                }
                if (advertisement.file != null)
                {
                    FileInfo fi = new FileInfo(advertisement.file.FileName);
                    var newFilename = "P" + advertisement.Id + "_" + string.Format("{0:d}",
                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    var webPath = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\images\ads\" + newFilename);
                    var pathToSave = @"/images/ads/" + newFilename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        advertisement.file.CopyTo(stream);
                    }
                    advertisement.ImageUrl = pathToSave;
                }
                var offer_StartDate = DateTime.Parse(formCollection["offer_StartDate"]);
                var offer_EndDate = DateTime.Parse(formCollection["offer_EndDate"]);
                advertisement.PublishDate = offer_StartDate;
                advertisement.EndDate = offer_EndDate;
                advertisement.AdvertisementOwner = null;
                db.Update(advertisement);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                if (!AdvertisementExists(advertisement.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["AdvertisementCategoryId"] = new SelectList(db.AdvertisementCategory.Where(x => !x.IsDeleted), "Id", "Name", advertisement.AdvertisementCategoryId);
                    ViewData["AdvertisementImageTypeId"] = new SelectList(db.AdvertisementImageTypes.Where(x => !x.IsDeleted), "Id", "Name", advertisement.AdvertisementImageTypeId);
                    ViewData["AdvertisementOwnerId"] = new SelectList(db.AdvertisementOwner.Where(x => !x.IsDeleted), "Id", "Email", advertisement.AdvertisementOwnerId);
                    ViewData["AdvertisementTypeId"] = new SelectList(db.AdvertisementType.Where(x => !x.IsDeleted), "Id", "DisplayName", advertisement.AdvertisementTypeId);
                    return View(advertisement);
                }
            }
        }

        public async Task<IActionResult> AcceptAd(int id)
        {
            try
            {
                if (!AdvertisementExists(id))
                {
                    return NotFound();
                }
                Advertisement advertisement = db.Advertisement.Find(id);
                if (advertisement == null)
                {
                    return NotFound();
                }
                advertisement.Status = "Accepted";
                db.Update(advertisement);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> RejectAd(int id)
        {
            try
            {
                if (!AdvertisementExists(id))
                {
                    return NotFound();
                }
                Advertisement advertisement = db.Advertisement.Find(id);
                if (advertisement == null)
                {
                    return NotFound();
                }
                advertisement.Status = "Rejected";
                db.Update(advertisement);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await db.Advertisement
                .Include(a => a.AdvertisementCategory)
                .Include(a => a.AdvertisementImageType)
                .Include(a => a.AdvertisementOwner)
                .Include(a => a.AdvertisementType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Admin/Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await db.Advertisement.FindAsync(id);
            db.AdvertisementProperty.RemoveRange(advertisement.AdvertisementProperty);
            await db.SaveChangesAsync();
            db.AirportAds.RemoveRange(advertisement.AirportAds);
            await db.SaveChangesAsync();
            db.Advertisement.Remove(advertisement);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(RequestedIndex));
        }

        private bool AdvertisementExists(int id)
        {
            return db.Advertisement.Any(e => e.Id == id);
        }
    }
}
