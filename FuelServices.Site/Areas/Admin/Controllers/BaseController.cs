using DBContext.Models;
using FuelServices.Site.Helpers.Configurations;
using FuelServices.Site.Helpers.Extensions;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BaseController : Controller
    {
        protected AirportCoreContext db;
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<ApplicationRole> RoleManager { get; private set; }
        protected IHttpContextAccessor httpContextAccessor;
        private IOptions<MyConfiguration> config;

        public BaseController()
        {
        }

        public BaseController(AirportCoreContext airportCoreContext)
        {
            db = airportCoreContext;
        }

        public BaseController(AirportCoreContext _db, IServiceProvider serviceProvider)
        {
            db = _db;
            UserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            httpContextAccessor = serviceProvider.GetService<HttpContextAccessor>();
            config = serviceProvider.GetService<IOptions<MyConfiguration>>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Dictionary<string, string> sessionActive = HttpContext.Session.GetComplexData<Dictionary<string, string>>("Active");
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            //if (sessionActive == null)
            //{
            sessionActive = new Dictionary<string, string>
                {
                    { "ContentManagements", "" },
                    { "ContentManagements.Index", "" },
                    { "ContentManagements.Create", "" },

                    { "FuelSuppliers", "" },
                    { "FuelSuppliers.Index", "" },
                    { "FuelSuppliers.Create", "" },

                    { "Customers", "" },
                    { "Customers.Index", "" },
                    { "Customers.Create", "" },

                    { "PaymentPackageFeatures", "" },
                    { "PaymentPackageFeatures.Index", "" },
                    { "PaymentPackageFeatures.Details", "" },

                    { "ContactUs", "" },
                    { "ContactUs.Index", "" },
                    { "ContactUs.Details", "" },

                    
                    { "AdvertisementCategories", "" },
                    { "AdvertisementOwners", "" },
                    { "Advertisements", "" },
                    { "Advertisements.Index", "" },
                    { "Advertisements.RequestedIndex", "" },
                    { "Advertisements.ArchivedIndex", "" },
                    


                    //
                    { "Airports", "" },
                    { "Cities", "" },
                    { "ColorPalettes", "" },
                    { "Companies", "" },
                    { "Contacts", "" },
                    { "Continents", "" },
                    { "Countries", "" },
                    { "Features", "" },
                    { "Home", "" },
                    { "Logs", "" },
                    { "MainCategories", "" },
                    { "News", "" },
                    { "PaymentPackages", "" },
                    { "Properties", "" },
                    { "Services", "" },
                    { "SubCategories", "" },
                    { "Suppliers", "" },
                    { "SupplierTypes", "" },
                    { "UserSpecializations", "" }
                };
            //}

            SetActive(sessionActive, controllerName, actionName);
            HttpContext.Session.SetComplexData("Active", sessionActive);
            HttpContext.Session.SetString("ControllerName", controllerName);
            HttpContext.Session.SetString("ActionName", actionName);

            if (db == null)
            {
                db = context.HttpContext.RequestServices.GetService<AirportCoreContext>();
            }
            int TodayCustomerCount = db.Customer.Where(o => !o.IsDeleted && o.Created == DateTime.Today)?.ToList()?.Count() ?? 0;
            DateTime d = DateTime.Today.AddDays(-6);
            int WeekCustomerCount = db.Customer.Where(o => !o.IsDeleted && o.Created < DateTime.Today
            && o.Created >= d).Count();

            HttpContext.Session.SetInt32("TodayCustomer", TodayCustomerCount);
            HttpContext.Session.SetInt32("WeekCustomer", WeekCustomerCount);

            if ((TodayCustomerCount + WeekCustomerCount) > 0)
            {
                HttpContext.Session.SetInt32("NotificationCount", TodayCustomerCount + WeekCustomerCount);
            }

            HttpContext.Session.SetComplexData("ContactUs", db.ContactUs.Where(c => !c.IsDeleted && c.Created.Date == DateTime.Today)
            .OrderByDescending(c => c.Created).ToList());

            HttpContext.Session.SetInt32("UnReadContactUsCount", db.ContactUs.Select(c => !c.IsDeleted && !c.IsRead).ToList().Count);

            // HttpContext.Session.SetInt32("RequestedAdsCount", db.Advertisement.Where(x => !x.IsDeleted
            // && x.Status == "Requested").Count());
            // HttpContext.Session.SetInt32("PiblishedAdsCount", db.Advertisement.Where(x => !x.IsDeleted
            //&& (x.Status == "Piblished" || x.Status == "Pending")).Count());
            // HttpContext.Session.SetInt32("ArchivedAdsCount", db.Advertisement.Where(x => x.IsDeleted
            //|| x.Status == "Archived" || x.Status == "Rejected").Count());

            // if (controllerName == "Advertisements" || controllerName == "Home")
            // {
            //     foreach (var item in db.Advertisement.Where(x => !x.IsDeleted)
            //     .Where(x => x.Status == "Accepted"))
            //     {
            //         if (item.PublishDate <= DateTime.Now && item.EndDate <= DateTime.Now)
            //         {
            //             item.Status = "Archived";
            //         }
            //         else if (item.PublishDate <= DateTime.Now && item.EndDate >= DateTime.Now)
            //         {
            //             item.Status = "Piblished";
            //         }
            //         else if (item.PublishDate >= DateTime.Now && item.EndDate >= DateTime.Now)
            //         {
            //             item.Status = "Pending";
            //         }
            //         db.Entry(item).State = EntityState.Modified;
            //         db.SaveChanges();
            //     }
            // }
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public Toast Message
        {
            get { return TempData.Get<Toast>("Message"); }

            set { TempData.Put("Message", value); }
        }

        protected string GetExceptionMessage(Exception e)
        {
            if (config == null)
            {
                config = HttpContext.RequestServices.GetService<IOptions<MyConfiguration>>();
            }
            var ExceptionMessageFormSettings = config.Value.ExceptionMessage;
            bool GetExceptionMessage = false;
            if (ExceptionMessageFormSettings != null)
                GetExceptionMessage = true;
            if (GetExceptionMessage)
            {
                if (e.InnerException != null)
                {
                    return e.InnerException.Message;
                }
                return e.Message;
            }
            return Toast.ErrorToast().Message;
        }

        protected Task<ApplicationUser> GetCurrentUserAsync() => UserManager.GetUserAsync(HttpContext.User);

        protected async Task<string> GetCurrentUserIdAsync() => (await UserManager.GetUserAsync(HttpContext.User)).Id;

        protected void SetActive(Dictionary<string, string> sessionActive, string controllerName, string actionName)
        {
            if (controllerName == "AirportContacts" || controllerName == "AirportContactPersons" ||
                controllerName == "AirportContactPersonContacts")
            {
                controllerName = "Airports";
            }
            if (controllerName == "AdvertisementProperties" || controllerName == "AirportAds")
            {
                controllerName = "Advertisements";
            }
            if (sessionActive != null)
            {
                sessionActive[controllerName] = "active";
                sessionActive[controllerName + "." + actionName] = "active";
                List<string> keys = new List<string>(sessionActive.Keys.Where(k => !k.Equals(controllerName)
                && !k.Equals(controllerName + "." + actionName)));
                foreach (var item in keys)
                {
                    sessionActive[item] = "";
                }
            }
        }

        protected T GetService<T>() where T : class => HttpContext.RequestServices.GetRequiredService<T>();
    }
}