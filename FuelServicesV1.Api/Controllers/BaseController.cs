

using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FuelServices.Api.Helpers;
using Microsoft.Extensions.Options;
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
    }
}
