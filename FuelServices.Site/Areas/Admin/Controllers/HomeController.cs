using Site.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Site.Helpers;
using System;
using Microsoft.AspNetCore.Identity;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(AirportCoreContext airportCoreContext,IServiceProvider serviceProvider) : base(airportCoreContext,serviceProvider)
        {
        }

        public IActionResult Index()
        {
            //StatisticsHelper statisticsHelper = new StatisticsHelper(_context);
            //ViewBag.Customers = statisticsHelper.CalculateCustomers();
            //ViewBag.Subscriptions = statisticsHelper.CalculateSubscription();
            //ViewBag.Ads = statisticsHelper.CalculateAds();
            //ViewBag.Suppliers = statisticsHelper.CalculateSuppliers();
            //ViewBag.SearchTerms = _context.SearchTerm.ToList();
            //ViewBag.PaymentPackages = statisticsHelper.CalculatePaymentPackages();
            //ViewBag.CalculateAdsDetails = statisticsHelper.CalculateAdsDetails();
            return View();
        }
        // add install and add roles
        //[OverrideAuthorization]
        [HttpGet]
        public async Task<ActionResult> InstallSupplierRoleAndUser()
        {



            // check if has at least one admin
            ApplicationRole role = await RoleManager.FindByNameAsync("Supplier");

            if (role == null)
            {
                role = await CreateNewRole("Supplier");
                
            }
            //City city = db.Cities.Where(s => s.Name_en == "city 1").FirstOrDefault();
            if (role != null)
            { 
                // create new user
                // check if admin@admin.sy exist
                ApplicationUser userexist = await UserManager.FindByEmailAsync("baraa.habib321@gmail.com");
                if (userexist == null)
                {
                    var newuser = new ApplicationUser
                    {
                        UserName = "baraa.habib321@gmail.com",
                        Email = "baraa.habib321@gmail.com",
                        PhoneNumber = "xxxxxx",
                        EmailConfirmed = true,
                        
                    };

                    var result = await UserManager.CreateAsync(newuser, "123456");

                    if (result.Succeeded)
                    {

                        await UserManager.AddToRoleAsync(newuser, "Supplier");

                        FuelSupplier fuelSupplier = new FuelSupplier()
                        {
                            Name = "Supplier",
                            UserId = newuser.Id,
                            IsDeleted = false,
                            IsMiddler = false,
                            CountryId = 73,
                        };
                        db.Add(fuelSupplier);
                        await db.SaveChangesAsync();
                        //await _signInManager.SignInAsync(newuser, isPersistent: false);

                        TempData["Success"] = "تم إنشاء مدير النظام بنجاح... كلمة المرور 123456";
                    }

                }
                else
                {
                    if (!(await UserManager.IsInRoleAsync(userexist, "Supplier")))
                        await UserManager.AddToRoleAsync(userexist, "admin");
                }

            }

            return RedirectToAction("Index");
        }
        private async Task<ApplicationRole> CreateNewRole(string name)
        {
            ApplicationRole role = await RoleManager.FindByNameAsync(name);
            if (role == null)
            {
                ApplicationRole newrole = new ApplicationRole();
                newrole.Name = name;
                await RoleManager.CreateAsync(newrole);
                return await RoleManager.FindByNameAsync(name);
            }
            return null;

        }

       
    }
}