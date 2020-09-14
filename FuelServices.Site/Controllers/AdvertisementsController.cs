//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DBContext.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json.Linq;
//using Site.DTOs;
//using X.PagedList;

//namespace Site.Controllers
//{
//    public class AdvertisementsController : BaseController
//    {

//        private readonly IHostingEnvironment _hostingEnvironment;
//        private string userId;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public AdvertisementsController(AirportCoreContext context,IServiceProvider provider) : base(context, provider)
//        {
//            _hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
//        }

//        // GET: Advertisements/Create
//        public IActionResult Create()
//        {
//            var AdTypes = db.AdvertisementType.Where(a => !a.IsDeleted);
//            var AdImagesTypes = db.AdvertisementImageType.Where(a => !a.IsDeleted);
//            ViewData["AdvertisementCategoryId"] = new SelectList(db.AdvertisementCategory.Where(a => !a.IsDeleted), "Id", "Name");
//            ViewData["AdvertisementImageTypeId"] = new SelectList(AdImagesTypes.Where(a => !a.IsDeleted), "Id", "Name");
//            ViewData["AdvertisementTypeId"] = new SelectList(AdTypes.Where(a => !a.IsDeleted), "Id", "DisplayName");
//            return View();
//        }

//        // POST: Advertisements/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        public async Task<IActionResult> PostCreate(Advertisement advertisement, IFormCollection pairs)
//        {
//            try
//            {
//                var AdTypes = db.AdvertisementType.Where(a => !a.IsDeleted);
//                var AdImagesTypes = db.AdvertisementImageType.Where(a => !a.IsDeleted);
//                ViewData["AdvertisementCategoryId"] = new SelectList(db.AdvertisementCategory.Where(a => !a.IsDeleted), "Id", "Name");
//                ViewData["AdvertisementImageTypeId"] = new SelectList(AdImagesTypes.Where(a => !a.IsDeleted), "Id", "Name");
//                ViewData["AdvertisementTypeId"] = new SelectList(AdTypes.Where(a => !a.IsDeleted), "Id", "DisplayName");

//                if (advertisement.file != null)
//                {
//                    FileInfo fi = new FileInfo(advertisement.file.FileName);
//                    var newFilename = "P" + advertisement.Id + "_" + string.Format("{0:d}",
//                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
//                    var webPath = _hostingEnvironment.WebRootPath;
//                    var path = Path.Combine("", webPath + @"\images\ads\" + newFilename);
//                    var pathToSave = @"/images/ads/" + newFilename;
//                    using (var stream = new FileStream(path, FileMode.Create))
//                    {
//                        advertisement.file.CopyTo(stream);
//                    }
//                    advertisement.ImageUrl = pathToSave;
//                }

//                if (HttpContext.User.Identity.IsAuthenticated)
//                {
//                    userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//                }

//                AdvertisementOwner advertisementOwner;
//                if (userId != null)
//                {
//                    advertisementOwner = db.AdvertisementOwner.Where(a => a.UserId == userId).FirstOrDefault();
//                    if (advertisementOwner != null)
//                    {
//                        advertisement.AdvertisementOwnerId = advertisementOwner.Id;
//                        advertisement.AdvertisementOwner = null;
//                    }
//                }
//                else
//                {
//                    return Unauthorized();
//                }
//                int selectedRange = 1;
//                List<Airport> airports = new List<Airport>();
//                if (advertisement.AdvertisementTypeId + "" == Constants.AD_TYPE_SIDE_BANNER)
//                {
//                    if (pairs["airport_ad"].ToString() == "1")
//                    {
//                        selectedRange = 1;
//                        int CountryId = int.Parse(pairs["CountryId"].ToString());
//                        airports = db.Airport.Where(a => !a.IsDeleted && a.CountryId == CountryId).ToList();
//                    }
//                    if (pairs["airport_ad"].ToString() == "2")
//                    {
//                        selectedRange = 2;
//                        int CityId = int.Parse(pairs["CityId"].ToString());
//                        airports = db.Airport.Where(a => !a.IsDeleted && a.CityId == CityId).ToList();
//                        if (airports == null || airports.Count() == 0)
//                        {
//                            City city = db.City.Find(CityId);
//                            airports = db.Airport.Where(a => !a.IsDeleted && a.CountryId == city.CountryId).ToList();
//                        }
//                    }
//                    if (pairs["airport_ad"].ToString() == "3")
//                    {
//                        selectedRange = 3;
//                        string[] AirportsIds = pairs["AirportsId"].ToString().Split(",");
//                        foreach (var item in AirportsIds)
//                        {
//                            int id = int.Parse(item);
//                            Airport airport = db.Airport.Find(id);
//                            airports.Add(airport);
//                        }
//                    }
//                }

//                advertisement.Status = "Requested";
//                advertisement.IsDeleted = false;
//                advertisement.CaptionClicks = 0;
//                advertisement.TotalPrice = 0;
//                advertisement.AdvertisementOwner = null;
//                db.Add(advertisement);
//                await db.SaveChangesAsync();

//                foreach (var item in airports)
//                {
//                    AirportAds airportAd = new AirportAds();
//                    airportAd.AdvertisementId = advertisement.Id;
//                    airportAd.AirportId = item.Id;
//                    airportAd.CaptionClicks = 0;
//                    airportAd.Range = selectedRange;
//                    airportAd.IsDeleted = false;
//                    db.AirportAds.Add(airportAd);
//                    await db.SaveChangesAsync();
//                }
//                return RedirectToAction("Create", "AdvertisementProperties", new { id = advertisement.Id });
//            }
//            catch (Exception e)
//            {
//                return RedirectToAction("Create", "Advertisements");
//            }
//        }

//        [HttpGet]
//        public Select2DTO Search(string q, int t, int? page)
//        {
//            List<Select2ResultDTO> result = new List<Select2ResultDTO>();

//            if (t == 3)
//            {
//                foreach (var item in db.Airport.Include(a => a.City.Country.Continent).Where(x => x.Name.Contains(q) || x.Iata.Contains(q) || x.Icao.Contains(q)).ToPagedList(page ?? 1, 30))
//                {
//                    Select2ResultDTO airport = new Select2ResultDTO();
//                    airport.id = item.Id.ToString();
//                    airport.text = item.Name;
//                    result.Add(airport);
//                }
//            }
//            if (t == 2)
//            {
//                foreach (var item in db.City.Include(a => a.Country.Continent).Where(x => x.Name.Contains(q)).ToPagedList(page ?? 1, 30))
//                {
//                    Select2ResultDTO city = new Select2ResultDTO();
//                    city.id = item.Id.ToString();
//                    city.text = item.Name;
//                    if (!result.Contains(city))
//                    {
//                        result.Add(city);
//                    }
//                }
//            }
//            if (t == 1)
//            {
//                foreach (var item in db.Country.Include(a => a.Continent).Where(x => x.Name.Contains(q)).ToPagedList(page ?? 1, 30))
//                {
//                    Select2ResultDTO country = new Select2ResultDTO();
//                    country.id = item.Id.ToString();
//                    country.text = item.Name;
//                    if (!result.Contains(country))
//                    {
//                        result.Add(country);
//                    }
//                }
//            }

//            IPagedList<Select2ResultDTO> pagedResult = result.ToPagedList(page ?? 1, 90);
//            Select2PaginateDTO select2PaginateDTO = new Select2PaginateDTO();
//            select2PaginateDTO.more = pagedResult.HasNextPage;
//            Select2DTO select2DTO = new Select2DTO();
//            select2DTO.results = pagedResult.ToList();
//            select2DTO.paginate = select2PaginateDTO;
//            return select2DTO;
//        }
//    }

//}
