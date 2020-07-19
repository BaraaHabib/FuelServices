using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.DTOs;
using X.PagedList;
using Microsoft.Extensions.DependencyInjection;
using FuelServices.Site.Helpers.Configurations;
using Microsoft.Extensions.Options;
using FuelServices.Site.Helpers.Toast;
using FuelServices.Site.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Site.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AirportCoreContext db;
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly RoleManager<ApplicationRole> roleManager;
        private readonly IOptions<MyConfiguration> config;

        private string userId;
        public string GetUserId()
        {
            userId = userId ?? GetCurrentUserId();
            return userId;
        }

        public BaseController(AirportCoreContext context, IServiceProvider provider)
        {
            db = context;
            userManager = provider.GetService<UserManager<ApplicationUser>>();
            roleManager = provider.GetService<RoleManager<ApplicationRole>>();
            config = provider.GetService<IOptions<MyConfiguration>>();
        }

        public BaseController(AirportCoreContext db)
        {
            this.db = db;
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
                    { "Home", "" },
                    { "Home.Privacy", "" },
                    { "Home.AboutUs", "" },
                    { "Home.Contact", "" },
                    { "Home.OurServices", "" },

                    { "PaymentPackages", "" },
                    { "PaymentPackages.Index", "" },
                    { "PaymentPackages.Details", "" },
                    { "PaymentPackages.Buy", "" },
                    { "PaymentPackages.BuyS", "" },


                    { "Offers", "" },
                    { "Offers.Index", "" },
                    { "Offers.Create", "" },

                    { "Requests", "" },
                    { "Requests.Create", "" },



                };
            //}
            SetActive(sessionActive, controllerName, actionName);
            HttpContext.Session.SetComplexData("Active", sessionActive);
            HttpContext.Session.SetString("ControllerName", controllerName);
            HttpContext.Session.SetString("ActionName", actionName);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            // send user image url
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(GetCurrentUserId());
                string imgUrl = "";
                if (User.IsInRole("Customer"))
                {
                    var customer = user.Customer;
                    imgUrl = customer?.ImageUrl ?? "";
                }
                else if (User.IsInRole("Supplier"))
                {
                    var supplier = user.FuelSupplier;
                    imgUrl = supplier?.ImageUrl ?? "";
                }

                ViewBag.UserImageUrl = imgUrl;
            }

            ////
            base.OnActionExecuted(context);
        }
        public Toast Message
        {
            get { return TempData.Get<Toast>("Message"); }

            set { TempData.Put("Message", value); }
        }

        [HttpGet]
        public Select2DTO SearchCountries(string q, int? page)
        {
            var query = (from a in db.Country.Where(x => !x.IsDeleted)
                         select a
                );
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(u => u.Name.Contains(q));
            }

            IPagedList<Country> pagedCountries = query.ToPagedList(page ?? 1, 30);
            List<Select2ResultDTO> result = new List<Select2ResultDTO>();
            foreach (var item in pagedCountries)
            {
                Select2ResultDTO temp = new Select2ResultDTO();
                temp.id = item.Id.ToString();
                temp.text = item.Name;
                result.Add(temp);
            }
            Select2PaginateDTO select2PaginateDTO = new Select2PaginateDTO();
            select2PaginateDTO.more = pagedCountries.HasNextPage;
            Select2DTO select2DTO = new Select2DTO();
            select2DTO.results = result;
            select2DTO.paginate = select2PaginateDTO;
            return select2DTO;
        }

        [HttpGet]
        public Select2DTO SearchCities(string q, int? page, string countryId)
        {
            var query = (from a in db.City.Where(x => !x.IsDeleted)
                         select a
                );
            if (!string.IsNullOrWhiteSpace(countryId))
            {
                query = query.Where(u => u.CountryId == int.Parse(countryId));
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(u => u.Name.Contains(q));
            }

            X.PagedList.IPagedList<City> pagedCities = query.ToPagedList(page ?? 1, 30);
            List<Select2ResultDTO> result = new List<Select2ResultDTO>();
            foreach (var item in pagedCities)
            {
                Select2ResultDTO temp = new Select2ResultDTO();
                temp.id = item.Id.ToString();
                temp.text = item.Name;
                result.Add(temp);
            }
            Select2PaginateDTO select2PaginateDTO = new Select2PaginateDTO();
            select2PaginateDTO.more = pagedCities.HasNextPage;
            Select2DTO select2DTO = new Select2DTO();
            select2DTO.results = result;
            select2DTO.paginate = select2PaginateDTO;
            return select2DTO;
        }

        protected Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);



        protected string GetExceptionMessage(Exception e)
        {
            var ExceptionMessageFormSettings = config?.Value?.ExceptionMessage;
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
        protected string GetExceptionMessage(Exception e, string DefaultMessage = null)
        {
            var ExceptionMessageFormSettings = config?.Value?.ExceptionMessage;
            bool GetExceptionMessage = false;
            if (ExceptionMessageFormSettings != null)
                GetExceptionMessage = true;
            if (GetExceptionMessage)
            {
                if (e.InnerException != null)
                    return e.InnerException.Message;
                return e.Message;
            }
            return DefaultMessage ?? Toast.ErrorToast().Message;
        }

        protected void SetActive(Dictionary<string, string> sessionActive, string controllerName, string actionName)
        {

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
    }
}