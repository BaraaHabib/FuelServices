using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Security.Claims;
using Site.Services;
using Site.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FuelServices.Site.Helpers.Toast;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Routing;
using Elect.Web.IUrlHelperUtils;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    public class HomeController : BaseController
    {
        private string userId;
        private readonly IEmailSender _emailSender;

        public HomeController(AirportCoreContext db,
            IEmailSender emailSender,
            IServiceProvider provider) : base(db, provider)
        {
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.what_we_offer = db.ContentManagement.Where(x => !x.IsDeleted).Where(a => a.Name == "what_we_offer").OrderBy(i => i.ItemOrder).ToList();

            var cs = db.Users.ToList();

            //cs.ForEach(async i =>
            //{
            //    if(i.User != null)
            //    {

            //        var us = i.User;
            //            await userManager.AddToRoleAsync(userManager.Users.First(x => x.Id == us.Id), "Customer");
            //    }
            //});
            //var sps = db.FuelSupplier.ToList();
            //sps.ForEach(async i =>
            //{
            //    if(i.User != null)
            //    {
            //        var us = i.User;
            //            await userManager.AddToRoleAsync(us, "Supplier");
            //    }
            //});
            foreach (var user in cs)
            {
                if (user.Customer != null)
                {

                    var us = user.Customer;
                    await userManager.AddToRoleAsync(user, "Customer");
                }
                else if (user.Customer == null && await userManager.IsInRoleAsync(user, "Customer"))
                {
                    var c = new Customer()
                    {
                        CountryId = 233,
                        ImageUrl = "/uploads/P51_10508883.jpg",
                        FirstName = "customer" + cs.IndexOf(user),
                        LastName = "customer" + cs.IndexOf(user),
                        UserId = user.Id
                    };
                    db.Add(c);
                    db.SaveChanges();
                }
            }
            foreach (var user in cs)
            {
                if (user.FuelSupplier != null)
                {

                    await userManager.AddToRoleAsync(user, "Supplier");
                }
                else if (user.FuelSupplier == null && await userManager.IsInRoleAsync(user, "Supplier"))
                {
                    var c = new FuelSupplier()
                    {
                        CountryId = 233,
                        ImageUrl = "/uploads/suppliers/AERO-Specialties-logo.jpg",
                        Name = "Supplier" + cs.IndexOf(user),
                        UserId = user.Id,
                        IsMiddler = false

                    };
                    db.Add(c);
                    db.SaveChanges();
                }
            };
            return View();
        }

        public async Task<IActionResult> OurServices()
        {
            var ourServices = await db.ContentManagement.Where(x => !x.IsDeleted).Where(a => a.Name == "our_services").OrderBy(i => i.ItemOrder).ToListAsync();
            return View(ourServices);
        }


        public IActionResult Contact()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            Customer customer = new Customer();
            if (userId != null)
            {
                customer = db.Customer.Where(c => c.UserId == userId).FirstOrDefault();
            }

            if (customer.User != null)
            {
                ViewBag.Customer = customer;
                ContactUs contactUs = new ContactUs();
                contactUs.CustomerId = customer.Id;
                contactUs.FirstName = customer.FirstName;
                contactUs.LastName = customer.LastName;
                contactUs.Email = customer.User.Email;
                contactUs.Tel = customer.User.PhoneNumber;
                return View(contactUs);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactUs contactUs)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            Customer customer = new Customer();
            if (userId != null)
            {
                customer = db.Customer.Where(c => c.UserId == userId).FirstOrDefault();
            }

            if (customer != null)
            {
                contactUs.CustomerId = customer.Id;
            }
            contactUs.SubmitDate = DateTime.UtcNow;
            db.ContactUs.Add(contactUs);
            await db.SaveChangesAsync();
            var simpleResponse = EmailSender.SendEmail(contactUs);
            Message = Toast.SucsessToastFront(simpleResponse.Message);
            //TempData.Set("Toast", simpleResponse);
            return RedirectToAction("Index");
        }

        public void Test()
        {
            var db1 = HttpContext.RequestServices.GetService<AirportCoreContext>();
            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Log log = new Log()
                {
                    TimeStamp = DateTimeOffset.UtcNow,
                    MessageTemplate = "Serilog Error",
                    Message = ""

                };
                db1.Log.Add(log);
                db1.SaveChanges(); 
                Debug.Print(msg);
                Debugger.Break();
            });



            //var user = await GetCurrentUserAsync();

            //var reqs = db.RequestOffers.Where(x => x.Request.Customer == user.Customer).ToList();

            //for (int i = 0; i < 2; i++)
            //{
            //    RequestOffers item = reqs[i];
            //    item.RStatus = ReplyStatus.ApprovedBySupplier;
            //    item.SupplierConfirmDate = DateTime.UtcNow.AddHours(-24);
            //    db.Update(item);
            //    await db.SaveChangesAsync();
            //}



            // Email test
            /*
            var contentAppName = db.ContentManagement.Where(cm => cm.Name == "app_name")
                           .FirstOrDefault();
            string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;
            EmailBodyDefaultParams emailBodyDefaultParams = db.EmailBodyDefaultParams
                .Where(e => e.EmailTypeName == "supplier_request_notification").FirstOrDefault();
            string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);

            //var supplierEmail = selectedOffer.FuelSupplier.User.Email;
            var callbackUrl = Url.AbsoluteAction("Details", "Requests", new { Area = "Supplier", id = 3003 });
            body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));
            var simpleResponse = EmailSender.SendEmailNotification("baraa.habib321@gmail.com", AppName, body);
            byte[] data = Encoding.UTF8.GetBytes(body);
            await Response.Body.WriteAsync(data, 0, data.Length);
            */

            //return Content(,MediaTypeHeaderValue.);
        }
        public bool RemovePackageClaims()
        {
            var cuss = db.CustomerPackagesLog.ToList();
            db.RemoveRange(cuss);
            var sips = db.SupplierPackagesLog.ToList();
            db.RemoveRange(sips);
            db.SaveChanges();

            var usersc = userManager.Users.ToList();

            usersc.ForEach(async x =>
            {
                var claims = await userManager.GetClaimsAsync(x);
                foreach (var claim in claims)
                {
                    if (claim.Type == "PackageExpiryDate" || claim.Type == "PaymentPackageId" ||
                        claim.Type == "PaymentPackageLevel")
                    {
                        await userManager.RemoveClaimAsync(x, claim);
                    }
                }
            });


            Serilog.Log.Information("test");
            return true;
        }

        public async Task<IActionResult> AboutUs()
        {
            var prs = await db.ContentManagement.Where(x => !x.IsDeleted && x.IsVisible).Where(x => x.Name == "about_us").ToListAsync();
            return View(prs);
        }

        public async Task<IActionResult> Privacy()
        {
            var prs = await db.ContentManagement.Where(x => !x.IsDeleted && x.IsVisible).Where(x => x.Name == "Privacy").ToListAsync();
            return View(prs);
        }

    }
}