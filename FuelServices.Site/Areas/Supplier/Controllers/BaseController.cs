using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using FuelServices.Site.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using FuelServices.Site.Helpers.Toast;
using FuelServices.Site.Helpers.Configurations;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    [Authorize(Roles = "Supplier")]
    public class BaseController : Controller
    {
        protected readonly AirportCoreContext db;
        public UserManager<ApplicationUser> UserManager { get; private set; }
        protected IHttpContextAccessor httpContextAccessor;
        private readonly IOptions<MyConfiguration> config;

        public BaseController() { }

        public BaseController(AirportCoreContext _db, IServiceProvider serviceProvider)
        {
            db = _db;
            UserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            httpContextAccessor = serviceProvider.GetService<HttpContextAccessor>();
            config = serviceProvider.GetService<IOptions<MyConfiguration>>();
        }
        public BaseController(AirportCoreContext airportCoreContext)
        {
            db = airportCoreContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Dictionary<string, string> sessionActive = HttpContext.Session.GetComplexData<Dictionary<string, string>>("Active");
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            //if (sessionActive == null)
            //{
                sessionActive = new Dictionary<string, string>()
                {
                 { "Offers", "" },
                 { "Offers.Index", "" },
                 { "Offers.Create", "" },
                 { "Home", "" },
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
            base.OnActionExecuted(context);
        }
        protected Task<ApplicationUser> GetCurrentUserAsync() => UserManager.GetUserAsync(HttpContext.User);

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

        public Toast Message
        {
            get { return TempData.Get<Toast>("Message"); }

            set { TempData.Put("Message", value); }
        }

        protected string GetExceptionMessage(Exception e)
        {
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

    }
}