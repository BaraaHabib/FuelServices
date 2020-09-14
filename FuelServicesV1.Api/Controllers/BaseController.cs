using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Helpers.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected AirportCoreContext db;
        protected UserManager<ApplicationUser> UserManager;
        protected SignInManager<ApplicationUser> SignInManager;
        protected RoleManager<ApplicationRole> RoleManager;
        protected readonly IOptions<MyConfiguration> config;

        
        private string AppName;

        /// <summary>
        /// get application name from database 
        /// </summary>
        /// <returns>application name from database or "Fuel Services" if their is not any name</returns>
        public string GetAppName()
        {
            // set up email notification
            var contentAppName = db.ContentManagement.Where(cm => cm.Name == "app_name")
                     .FirstOrDefault();
            AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;
            return AppName;
        }

        public BaseController(IServiceProvider serviceProvider)
        {
            db = serviceProvider.GetService<AirportCoreContext>();
            UserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            SignInManager = serviceProvider.GetService<SignInManager<ApplicationUser>>();
            RoleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            config = serviceProvider.GetService<IOptions<MyConfiguration>>();
        }

        private string GetModelErrors(ModelStateDictionary ModelState)
        {
            string message = "";
            ModelState.Values.ToList().ForEach(
                x =>
                {
                    message += x.Errors.Select(s => s.ErrorMessage).Aggregate((a, b) => a + b) + "\n";
                });
            return message;
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
            return "Some thing went wrong.";
        }

        protected Task<ApplicationUser> GetCurrentUserAsync() => UserManager.GetUserAsync(HttpContext.User);

        protected T GetService<T>() where T : class => HttpContext.RequestServices.GetRequiredService<T>();
    }
}