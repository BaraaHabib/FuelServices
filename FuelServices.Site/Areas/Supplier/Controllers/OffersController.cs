using AutoMapper;
using DBContext.Models;
using FuelServices.DBContext.DatatablesModels;
using FuelServices.DBContext.Domain;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    public class OffersController : BaseController
    {
        public OffersController(AirportCoreContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        // GET: Supplier/Offers
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetData()
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

                var user = await GetCurrentUserAsync();
                var tableData = user.FuelSupplier?.Offer?.AsQueryable().Where(d => !d.IsDeleted);
                //(from temp in db.Offer select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    tableData = tableData.Where(d => !d.IsDeleted).Where(m => m.StartDate.ToString().Contains(searchValue) ||
                    m.EndDate.ToString().Contains(searchValue) || m.Status.Contains(searchValue)
                    || m.DuesTaxesLevies.Contains(searchValue));
                }
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = GetService<IMapper>().Map<List<OfferDatatableViewModel>>(data)
                });
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Supplier/Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var offer = await db.Offer
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (offer == null)
                {
                    return NotFound();
                }

                return View(offer);
            }
            catch (Exception e)
            {
                Message = new Toast(
                     GetExceptionMessage(e),
                    ToasterType.error);
                return View();
            }
        }

        // GET: Supplier/Offers/Create
        public IActionResult Create()
        {
            var Continents = base.db.Continent.ToList();
            var Countries = db.Country.Where(d => !d.IsDeleted).Where(q => q.ContinentId == Continents.FirstOrDefault().Id).ToList();
            var Cities = db.City.Where(d => !d.IsDeleted).Where(q => q.CountryId == Countries.FirstOrDefault().Id).ToList();

            ViewData["ContinentId"] = new SelectList(Continents, "Id", "Name");
            ViewData["CountryId"] = new SelectList(Countries, "Id", "Name");
            ViewData["CityId"] = new SelectList(Cities, "Id", "Name");
            ViewData["AirportId"] = new SelectList(base.db.Airport.Take(20), "Id", "Name");

            ViewBag.FuelTypes = db.FuelType.ToList();
            OfferViewModel model = new OfferViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now.AddYears(1);
            return View(model);
        }

        // POST: Supplier/Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferViewModel model)
        {
            //var Continents = base.db.Continent.ToList();
            //var Countries = db.Country.Where(d => !d.IsDeleted).Where(q => q.ContinentId == Continents.FirstOrDefault().Id).ToList();
            //var Cities = db.City.Where(d => !d.IsDeleted).Where(q => q.CountryId == Countries.FirstOrDefault().Id).ToList();

            //ViewData["ContinentId"] = new SelectList(Continents, "Id", "Name");
            //ViewData["CountryId"] = new SelectList(Countries, "Id", "Name");
            //ViewData["CityId"] = new SelectList(Cities, "Id", "Name");
            ViewData["AirportId"] = new SelectList(base.db.Airport.Take(20), "Id", "Name");

            ViewBag.FuelTypes = db.FuelType.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.AirportOffers.Count == 0)
                    {
                        ModelState.AddModelError("", "Select at least one airport.");
                        throw new Exception();
                    }

                    //checkDublication(model);
                    var userId = (await GetCurrentUserAsync()).Id;

                    //string userId = User.Identity.GetUserId();
                    FuelSupplier fuelSupplier = db.FuelSupplier.Where(d => !d.IsDeleted).Where(e => e.UserId == userId).FirstOrDefault();
                    if (fuelSupplier == null)
                    {
                        return NotFound();
                    }
                    int supplierId = fuelSupplier.Id;

                    Offer offer = new Offer()
                    {
                        FuelSupplierId = supplierId,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Status = OfferStatus.Active.ToString(),
                        DuesTaxesLevies = model.DuesTaxesLevies,
                        ItemOrder = model.ItemOrder,
                    };
                    await base.db.Offer.AddAsync(offer);

                    await base.db.SaveChangesAsync();

                    List<OfferFuelType> offerFuelType = new List<OfferFuelType>();
                    model.FuelTypes.ForEach(
                        x => offerFuelType.Add(new OfferFuelType() { OfferId = offer.Id, FuelTypeId = x }
                    ));
                    await db.AddRangeAsync(offerFuelType);

                    await base.db.SaveChangesAsync();

                    List<AirportOffer> AirportOffers = new List<AirportOffer>();
                    model.AirportOffers.ForEach(
                         x =>
                         {
                             Airport airport = db.Airport.Find(x.AirportId);
                             var cityId = airport.CityId;
                             var countryId = airport.CountryId;
                             AirportOffer airportOffer = new AirportOffer()
                             {
                                 AirportId = airport.Id,
                                 CityId = cityId,
                                 Price = x.Price,
                                 PriceUnit = x.PriceUnit,
                                 OfferId = offer.Id,
                             };
                             AirportOffers.Add(airportOffer);
                         }
                    );
                    await db.AddRangeAsync(AirportOffers);

                    await base.db.SaveChangesAsync();

                    Message = Toast.SucsessToast();
                    return RedirectToAction("Details", new { offer.Id });
                }

                model.AirportOffers.ForEach(x => x.Airport = db.Airport.Find(x.AirportId));
                return View(model);
            }
            catch (Exception e)
            {
                ViewData["AirportId"] = new SelectList(base.db.Airport.Take(20), "Id", "Name");
                ModelState.AddModelError("", GetExceptionMessage(e));
                Serilog.Log.Error(e.Message);
                model.AirportOffers.ForEach(x => x.Airport = db.Airport.Find(x.AirportId));
                return View(model);
            }
        }

        // GET: Supplier/Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.FuelTypes = db.FuelType.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var offer = await db.Offer.FindAsync(id);
            if (offer == null || offer.IsDeleted)
            {
                return NotFound();
            }
            List<AirportOfferViewModel> AirportOffers = offer.AirportOffers.Where(q => q.AirportId != null)
                .Select(x => new AirportOfferViewModel()
                {
                    AirportId = (int)x.AirportId,
                    Airport = x.Airport,
                    OfferId = offer.Id,
                    Price = x.Price,
                    PriceUnit = x.PriceUnit
                }).ToList(); ;
            OfferViewModel model = new OfferViewModel()
            {
                Id = offer.Id,
                AirportOffers = AirportOffers,
                DuesTaxesLevies = offer.DuesTaxesLevies,
                EndDate = offer.EndDate,
                StartDate = offer.StartDate,
                SelectedAirports = offer.AirportOffers.Where(z => z.Airport != null).Select(x => x.Airport).ToList(),
                Created = offer.Created,
                Status = offer.Status,
                FuelSupplier = offer.FuelSupplier,
            };

            return View(model);
        }

        // POST: Supplier/Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfferViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            Offer offer = db.Offer.Find(id);
            if (ModelState.IsValid)
            {
                try
                {
                    offer.StartDate = model.StartDate;
                    offer.EndDate = model.EndDate;
                    offer.ItemOrder = model.ItemOrder;
                    offer.DuesTaxesLevies = model.DuesTaxesLevies;

                    // update parties
                    List<AirportOffer> AirportOffer = db.AirportOffers.Where(w => w.OfferId == id).ToList();
                    db.RemoveRange(AirportOffer);

                    List<AirportOffer> newAirportOffers = new List<AirportOffer>();
                    model.AirportOffers.ForEach(
                         x =>
                         {
                             Airport airport = db.Airport.Find(x.AirportId);
                             var cityId = airport.CityId;
                             var countryId = airport.CountryId;
                             AirportOffer airportOffer = new AirportOffer()
                             {
                                 AirportId = airport.Id,
                                 CityId = cityId,
                                 Price = x.Price,
                                 PriceUnit = x.PriceUnit,
                                 OfferId = model.Id,
                             };
                             newAirportOffers.Add(airportOffer);
                         }
                    );
                    await db.AddRangeAsync(newAirportOffers);

                    // update fuel types
                    List<OfferFuelType> offerFuelTypes = db.OfferFuelType
                        .Where(w => w.OfferId == id).ToList();
                    db.RemoveRange(offerFuelTypes);

                    List<OfferFuelType> newofferFuelType = new List<OfferFuelType>();
                    model.FuelTypes.ForEach(
                        x => newofferFuelType.Add(new OfferFuelType() { OfferId = model.Id, FuelTypeId = x }
                    ));
                    await db.AddRangeAsync(newofferFuelType);

                    db.Update(offer);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Message = Toast.SucsessToast();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.FuelTypes = db.FuelType.ToList();
            model.SelectedAirports = offer.AirportOffers.Where(z => z.Airport != null).Select(x => x.Airport).ToList();
            List<AirportOfferViewModel> AirportOffers = offer.AirportOffers.Where(q => q.AirportId != null)
                .Select(x => new AirportOfferViewModel()
                {
                    AirportId = (int)x.AirportId,
                    Airport = x.Airport,
                    OfferId = offer.Id,
                    Price = x.Price,
                    PriceUnit = x.PriceUnit
                }).ToList();
            model.AirportOffers = AirportOffers;
            return View(model);
        }

        // GET: Supplier/Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await db.Offer
                .Include(o => o.FuelSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Supplier/Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await db.Offer.FindAsync(id);
            offer.IsDeleted = true;
            db.Update(offer);
            await db.SaveChangesAsync();
            Message = Toast.SucsessToast();

            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return db.Offer.Any(e => e.Id == id);
        }

        private void AddAirportsOffers(OfferViewModel model, Offer offer)
        {
            foreach (var item in model.Airports)
            {
                AirportOffer AirportOffer = new AirportOffer()
                {
                    OfferId = offer.Id,
                    AirportId = item,
                };
                db.Add(AirportOffer);
            }
        }
    }
}