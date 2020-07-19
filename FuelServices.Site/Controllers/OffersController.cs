using Site.DTOs;
using Site.Helpers;
using Site.Models;
using Site.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace Site.Controllers
{
    public class OffersController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public OffersController(AirportCoreContext context, IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Advertisement> homeGridAdvertisements = await db.Advertisement.Where(a => a.AdvertisementType.Name
            == "home_page_grid" && a.EndDate >= DateTime.UtcNow).Where(a => a.AdvertisementProperty.Where(ap => ap.AdvertisementTypeProperty.Name == "in_home_page" 
            && ap.Value == "True").FirstOrDefault() != null).OrderBy(a => a.ItemOrder).ToListAsync();

            List<Advertisement> popupAdvertisements = db.Advertisement.Where(a => (a.AdvertisementType.Name
            == "popup_bottom_left" || a.AdvertisementType.Name == "popup_bottom_right" || a.AdvertisementType.Name == "popup_top_left"
            || a.AdvertisementType.Name == "popup_top_right" || a.AdvertisementType.Name == "popup_video" || a.AdvertisementType.Name
            == "popup_frame") && a.EndDate >= DateTime.UtcNow && a.AdvertisementProperty.Where(ap => ap.AdvertisementTypeProperty.Name
            == "in_home_page" && ap.Value == "True").FirstOrDefault() != null).ToList();

            ViewBag.popup = popupAdvertisements;
            ViewBag.home_page_grid = homeGridAdvertisements;
            ViewBag.Suppliers = db.FuelSupplier.Take(9).ToList();
            return View();
        }


        [HttpGet]
        public string ClickCount(int id)
        {
            Advertisement advertisement = db.Advertisement.Find(id);
            if (advertisement != null)
            {
                advertisement.CaptionClicks++;
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
            }
            return "";
        }

        protected IPagedList<Offer> GetPagedItems(int? page, string country, string city, string name, string airport)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            var query = from a in db.Offer select a;

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(u => (u.AirportOffers.Where(op => op.Airport.CountryId == int.Parse(country)).FirstOrDefault() != null) ||
                (u.AirportOffers.Where(op => op.Airport.City.CountryId == int.Parse(country)).FirstOrDefault() != null));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                int cityId = int.Parse(city);
                var c = db.City.Find(cityId);
                if (c != null)
                {
                    query = query.Where(u => u.AirportOffers.Where(op => op.Airport.CityId == cityId).FirstOrDefault() != null);
                    query = query.Where(u => u.AirportOffers.Where(op => op.Airport.Country.City.Contains(c)).FirstOrDefault() != null);
                }
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                int sId = int.Parse(name);
                query = query.Where(u => u.FuelSupplier.Id == sId);
            }

            if (!string.IsNullOrWhiteSpace(airport))
            {
                int airportId = int.Parse(airport);
                Airport a = db.Airport.Find(airportId);
                if (a != null)
                {
                    query = query.Where(u => u.AirportOffers.Where(aa => aa.AirportId == airportId).FirstOrDefault() != null);
                }
            }

            var listPaged = query.ToPagedList(page ?? 1, 6);

            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }
            return listPaged;
        }

        public IActionResult GetOnePageOfItems(int PageNumber = 1, string country = null, string city = null, string name = null, string airport = null)
        {
            var listPaged = GetPagedItems(PageNumber, country, city, name, airport);
            ViewBag.country = country;
            ViewBag.city = city;
            ViewBag.name = name;
            ViewBag.airport = airport;
            ViewBag.MinCustomerPackage = db.PaymentPackage.Where(pp => pp.Type == PackageType.CustomerPackage).
                Where(pp => pp.IsValid).OrderBy(pp => pp.ItemLevel).FirstOrDefault();
            ViewBag.MaxCustomerPackage = db.PaymentPackage.Where(pp => pp.Type == PackageType.CustomerPackage).
                Where(pp => pp.IsValid).OrderByDescending(pp => pp.ItemLevel).FirstOrDefault();
            return PartialView("_OffersListPartial", listPaged);
        }

        [HttpGet]
        public IActionResult Search(string id, string country, string city, string name, string airport)
        {
            if (id != null && id.Length > 3)
            {
                if (id.Substring(0, 3) == "su_")
                {
                    name = id.Substring(3);
                }
                else if (id.Substring(0, 3) == "ci_")
                {
                    city = id.Substring(3);
                }
                else if (id.Substring(0, 3) == "co_")
                {
                    country = id.Substring(3);
                }
                else if (id.Substring(0, 3) == "ai_")
                {
                    airport = id.Substring(3);
                }
            }
            return GetOnePageOfItems(1, country, city, name, airport);
        }

        [HttpGet]
        public Select2DTO SearchItems(string q, int? page)
        {
            
            try
            {
                List<Select2ResultDTO> result = new List<Select2ResultDTO>();

                foreach (var item in db.Airport.Include(a => a.City.Country.Continent).Where(x => x.Name.Contains(q) || x.Iata.Contains(q) || x.Icao.Contains(q)).ToPagedList(page ?? 1, 30))
                {
                    Select2ResultDTO airport = new Select2ResultDTO();
                    airport.id = "ai_" + item.Id.ToString();
                    airport.text = item.Name;
                    result.Add(airport);
                }

                foreach (var item in db.City.Include(a => a.Country.Continent).Where(x => x.Name.Contains(q)).ToPagedList(page ?? 1, 30))
                {
                    Select2ResultDTO city = new Select2ResultDTO();
                    city.id = "ci_" + item.Id.ToString();
                    city.text = item.Name;
                    if (!result.Contains(city))
                    {
                        result.Add(city);
                    }
                }

                foreach (var item in db.Country.Include(a => a.Continent).Where(x => x.Name.Contains(q)).ToPagedList(page ?? 1, 30))
                {
                    Select2ResultDTO country = new Select2ResultDTO();
                    country.id = "co_" + item.Id.ToString();
                    country.text = item.Name;
                    if (!result.Contains(country))
                    {
                        result.Add(country);
                    }
                }
                if (result.Count() == 0)
                {
                    foreach (var item in db.Airport.Include(a => a.City.Country.Continent).ToPagedList(page ?? 1, 30))
                    {
                        Select2ResultDTO airport = new Select2ResultDTO();
                        airport.id = "ai_" + item.Id.ToString();
                        airport.text = item.Name;
                        result.Add(airport);
                    }
                }
                //if (q != null)
                //{
                //    if (q.Length >= 3)
                //    {
                //        var oldTerm = db.SearchTerm.Where(st => st.Keyword == q).FirstOrDefault();
                //        if (oldTerm != null)
                //        {
                //            oldTerm.Count++;
                //            db.Entry(oldTerm).State = EntityState.Modified;
                //            db.SaveChanges();
                //        }
                //        else
                //        {
                //            SearchTerm searchTerm = new SearchTerm();
                //            searchTerm.Keyword = q;
                //            searchTerm.Count = 1;
                //            db.SearchTerm.Add(searchTerm);
                //            db.SaveChanges();
                //        }
                //    }
                //}
                IPagedList<Select2ResultDTO> pagedResult = result.ToPagedList(page ?? 1, 90);
                Select2PaginateDTO select2PaginateDTO = new Select2PaginateDTO();
                select2PaginateDTO.more = pagedResult.HasNextPage;
                Select2DTO select2DTO = new Select2DTO();
                select2DTO.results = pagedResult.ToList();
                select2DTO.paginate = select2PaginateDTO;
                return select2DTO;
            }
            catch (Exception e)
            {
                Select2DTO select2DTO = new Select2DTO();
                var Log = new Log()
                {
                    Message = e.Message,

                };
                db.Log.Add(Log);
                db.SaveChanges();
                return select2DTO;

                //throw;
            }

        }

        [HttpGet]
        public bool test()
        {
            return true;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int? id)
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

            ViewBag.FuelTypes = offer.OfferFuelType.Select(x => x.FuelType).ToList();

            return View(offer);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
