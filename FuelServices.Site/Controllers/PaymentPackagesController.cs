using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Stripe;
using Microsoft.Extensions.Options;
using FuelServices.Site.Helpers.Stripe;
using FuelServices.Site.Helpers.Toast;
using FuelServices.DBContext.Models;
using System.Dynamic;

namespace Site.Controllers
{
    public class PaymentPackagesController : BaseController
    {
        protected readonly IOptions<StripeSettings> StripeOptions; 
        public PaymentPackagesController(AirportCoreContext context,IServiceProvider provider, IOptions<StripeSettings>options) :base(context,provider)
        {
            this.StripeOptions = options;
        }

        // GET: PaymentPackages
        public async Task<IActionResult> Index()
        {
            return View(await db.PaymentPackage.Where(pp => pp.IsValid)
                    .OrderBy(pp => pp.ItemOrder).ToListAsync());

            if (User.IsInRole("Supplier"))
            {
                return View(await db.PaymentPackage.Where(pp => pp.IsValid).Where(p=>p.Type == PackageType.SupplierPackage)
                    .OrderBy(pp => pp.ItemOrder).ToListAsync());
            }
            else if (User.IsInRole("Customer"))
            {
                return View(await db.PaymentPackage.Where(pp => pp.IsValid).Where(p => p.Type == PackageType.CustomerPackage)
                    .OrderBy(pp => pp.ItemOrder).ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        // GET: PaymentPackages/Details/5
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details()
        {
            var user = await GetCurrentUserAsync();
            var customer = user.Customer;

            if (customer == null)
                return NotFound();

            var package = await db.CustomerPackagesLog
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.CustomerId == customer.Id);

            if (package == null || package.EndDate < DateTime.UtcNow)
                return NotFound();

            return View(package);
        }

        // GET: PaymentPackages/Details/5
        [Authorize(Roles = "Supplier")]
        public async Task<IActionResult> DetailsS()
        {
            var user = await GetCurrentUserAsync();
            var supplier = user.FuelSupplier;

            if (supplier == null)
                return NotFound();

            var package = await db.CustomerPackagesLog
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.CustomerId == supplier.Id);

            if (package == null || package.EndDate < DateTime.UtcNow)
                return NotFound();

            return View(package);
        }

        [Authorize(Roles ="Supplier,Customer")]
        public async Task<IActionResult> Buy(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }
            string userType = "";
            if (User.IsInRole("Customer"))
            {
                userType = "Customer";
            }
            if (User.IsInRole("Supplier"))
            {
                userType = "Supplier";
            }
            var paymentPackage = await db.PaymentPackage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentPackage == null)
            {
                return NotFound();
            }

            ViewData["PaymentPackageId"] = (int)id;
            ViewData["PaymentPackage"] = paymentPackage;

            try
            {
                #region Stripe
                // Set your secret key. Remember to switch to your live secret key in production!
                // See your keys here: https://dashboard.stripe.com/account/apikeys
                StripeConfiguration.ApiKey = StripeOptions?.Value?.SecretKey;
                
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (int)(paymentPackage.Price * 100),
                    Currency = paymentPackage.PriceUnit,
                    // Verify your integration in this guide by including this parameter
                    Metadata = new Dictionary<string, string>()
                        {
                          {"integration_check", "accept_a_payment"},
                          {"paymentPackageId", paymentPackage.Id.ToString()},
                          {"userId", userId},
                          {"userType", userType},
                        }
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);
                ViewBag.ClientSecret = paymentIntent.ClientSecret;

                #endregion
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Helpers.Constants.PAYMENT_ERROR, $"User : {User.Identity.Name}");
                Message = Toast.ErrorToastFront(GetExceptionMessage(e,"Payment Error. Contact site administrator."));
                //return Content(e.ToString());
            }


            if (User.IsInRole("Supplier"))
            {
                FuelSupplier fuelSupplier = db.FuelSupplier.Where(s => s.UserId == userId).FirstOrDefault();
                ViewData["Supplier"] = fuelSupplier;
                return View("BuyS");
            }
            else
            {
                DBContext.Models.Customer customer = db.Customer.Where(c => c.UserId == userId).FirstOrDefault();
                ViewData["Customer"] = customer;
                return View("Buy");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Buy(CustomerPackagesLog customerPackagesLog,
            int PaymentPackageId)
        {
            if (customerPackagesLog.Id != 0)
                customerPackagesLog.Id = 0;
            var paymentPackage = await db.PaymentPackage
                .FirstOrDefaultAsync(m => m.Id == PaymentPackageId);
            if (paymentPackage == null)
            {
                return NotFound();
            }

            ViewData["PaymentPackageId"] = (int)PaymentPackageId;
            ViewData["PaymentPackage"] = paymentPackage;

            try
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int OfferId = 0;
            if (TempData["OfferId"] != null)
            {
                OfferId = Convert.ToInt32(TempData["OfferId"]);
            }

            DBContext.Models.Customer customer = db.Customer.Where(c => c.UserId == userId).FirstOrDefault();
            
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(userId);

                    DateTime startDate = DateTime.UtcNow;
                    DateTime expiryDate = startDate.AddDays((int)paymentPackage.Period);

                    var claims = await userManager.GetClaimsAsync(user);
                    foreach (var claim in claims)
                    {
                        if (claim.Type == "PackageExpiryDate" || claim.Type == "PaymentPackageId" ||
                            claim.Type == "PaymentPackageLevel")
                        {
                            await userManager.RemoveClaimAsync(user, claim);
                        }
                    }
                    await userManager.AddClaimAsync(user, new Claim("PackageExpiryDate", expiryDate.ToString()));
                    await userManager.AddClaimAsync(user, new Claim("PaymentPackageId", PaymentPackageId.ToString()));
                    await userManager.AddClaimAsync(user, new Claim("PaymentPackageLevel", paymentPackage.ItemLevel.ToString()));

                    customerPackagesLog.StartDate = DateTime.UtcNow;
                    customerPackagesLog.EndDate = expiryDate;
                    customerPackagesLog.PaymentPackageId = PaymentPackageId;
                    customerPackagesLog.CustomerId = customer.Id;
                    db.CustomerPackagesLog.Add(customerPackagesLog);

                    await db.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    var url = Url.Action("Index", "Home", new { id = OfferId });

                    return Redirect(url);
                }
            }
            catch (Exception e)
            {
                Message = Toast.ErrorToastFront(GetExceptionMessage(e));
            }
            ViewData["PaymentPackageId"] = (int)PaymentPackageId;
            ViewData["PaymentPackage"] = paymentPackage;
            return View(customerPackagesLog);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supplier")]
        public async Task<IActionResult> BuyS(SupplierPackagesLog supplierPackagesLog,
            int PaymentPackageId)
        {
            if (supplierPackagesLog.Id != 0)
                supplierPackagesLog.Id = 0;
            var paymentPackage = await db.PaymentPackage
               .FirstOrDefaultAsync(m => m.Id == PaymentPackageId);
            if (paymentPackage == null)
            {
                return NotFound();
            }

            ViewData["PaymentPackageId"] = (int)PaymentPackageId;
            ViewData["PaymentPackage"] = paymentPackage;

            try
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int OfferId = 0;
            if (TempData["OfferId"] != null)
            {
                OfferId = Convert.ToInt32(TempData["OfferId"]);
            }

            DBContext.Models.FuelSupplier supplier = db.FuelSupplier.Where(c => c.UserId == userId).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(userId);

                    DateTime startDate = DateTime.UtcNow;
                    DateTime expiryDate = startDate.AddDays((int)paymentPackage.Period);

                    var claims = await userManager.GetClaimsAsync(user);
                    foreach (var claim in claims)
                    {
                        if (claim.Type == "PackageExpiryDate" || claim.Type == "PaymentPackageId" ||
                            claim.Type == "PaymentPackageLevel")
                        {
                            await userManager.RemoveClaimAsync(user, claim);
                        }
                    }
                    await userManager.AddClaimAsync(user, new Claim("PackageExpiryDate", expiryDate.ToString()));
                    await userManager.AddClaimAsync(user, new Claim("PaymentPackageId", PaymentPackageId.ToString()));
                    
                    await userManager.AddClaimAsync(user, new Claim("PaymentPackageLevel", paymentPackage.ItemLevel.ToString()));

                    supplierPackagesLog.StartDate = DateTime.UtcNow;
                    supplierPackagesLog.EndDate = expiryDate;
                    supplierPackagesLog.PaymentPackageId = PaymentPackageId;
                    supplierPackagesLog.FuelSupplierId = supplier.Id;
                    db.SupplierPackagesLog.Add(supplierPackagesLog);

                    await db.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    var url = Url.Action("Index", "Home");

                    return Redirect(url);
                }
            }
            catch (Exception e)
            {
                Message = Toast.ErrorToastFront(GetExceptionMessage(e));
            }

            return View(supplierPackagesLog);
        }

        public async Task<ActionResult> DisplayDetails(string url)
        {
            return await Task.Run<ActionResult>(() =>
            {
                if (true)
                {
                    return Content(url);
                }
            });
        }
    }
}
