using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using Site.Controllers;
using Microsoft.AspNetCore.Authorization;
using Site.DTOs;
using FuelServices.Site.Models;
using FuelServices.Site.Helpers.Toast;
using Site.Services;
using System.Text.Encodings.Web;
using Elect.Web.IUrlHelperUtils;
using Stripe;
using FuelServices.Site.Helpers.Stripe;
using Microsoft.Extensions.Options;
using Site.Helpers;

namespace FuelServices.Site.Controllers
{
    public class RequestsController : BaseController
    {
        protected readonly IOptions<StripeSettings> StripeOptions;

        public RequestsController(AirportCoreContext context,IServiceProvider provider, IOptions<StripeSettings> options) : base(context,provider)
        {
            this.StripeOptions = options;

        }

        // GET: Requests
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var customer = user.Customer;
                return View(customer.Request.ToList());
            }
            catch (Exception e)
            {
                Message = Toast.ErrorToast(GetExceptionMessage(e));
                return View(new List<Request>());
            }
        }

        // GET: Requests/Details/5
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            // TODO: chech authurization 
            if (id == null)
            {
                return NotFound();
            }

            var request = await db.Request
                .Include(r => r.Airport)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            ViewBag.SupplierId = request.RequestOffers.FirstOrDefault()?.Offer.FuelSupplierId;
            request?.RequestOffers.ToList().ForEach(
                x =>
                {
                    if(x.RStatus == ReplyStatus.ApprovedBySupplier)
                    {
                        ViewBag.RequestOfferId = x.Id;
                    }
                }
                );
            return View(request);
        }

        [Authorize(Roles ="Customer")]
        // GET: Requests/Create
        public async Task<IActionResult> Create()
        {
            var user = (await GetCurrentUserAsync());
            ViewBag.CustomerId = db.Customer.Where(c => c.UserId == user.Id).FirstOrDefault().Id;
            ViewBag.OfferFuelTypes =  new SelectList(db.FuelType.ToList(),"Id","Name");
            ViewBag.Offers = new List<Select2ResultDTO>();
            //RequestViewModel Request = new RequestViewModel();
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel model)
        {
            var user = (await GetCurrentUserAsync());
            try
            {

            model.Req.SendDate = DateTime.UtcNow;

            if (user.Customer.Id != model.Req.CustomerId)
            {
                return BadRequest();
            }

                if (ModelState.IsValid)
                {

                    // set up email notification
                    var contentAppName = db.ContentManagement.Where(cm => cm.Name == "app_name")
                             .FirstOrDefault();
                    string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;
                    EmailBodyDefaultParams emailBodyDefaultParams = db.EmailBodyDefaultParams
                        .Where(e => e.EmailTypeName == "supplier_request_notification").FirstOrDefault();
                    string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                    //

                    db.Request.Add(model.Req);
                    model.SelectedOffers.ForEach( offerId =>
                    {
                        var selectedOffer = db.Offer.Find(offerId);
                        var selectedOfferParty = selectedOffer.AirportOffers.Where(x => x.AirportId == model.Req.AirportId).FirstOrDefault();

                        RequestOffers requestOffers = new RequestOffers()
                        {
                            OfferId = offerId,
                            RequestId = model.Req.Id,
                            AirportOfferId = selectedOfferParty.Id,
                            RStatus = ReplyStatus.Pending,
                        };
                        db.Add(requestOffers);
                        db.SaveChanges();

                        #region send email
                        //get supllier

                        var supplierEmail = selectedOffer.FuelSupplier.User.Email;
                        var callbackUrl = Url.AbsoluteAction("Details", "Requests", new { Area = "Supplier", id = model.Req.Id });
                        body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));
                        var simpleResponse = EmailSender.SendEmailNotification(supplierEmail, AppName, body);
                        #endregion


                    });
                    //model.Req.


                    return RedirectToAction(nameof(Details),new {id = model.Req.Id });
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(GetExceptionMessage(e));
                ModelState.AddModelError("", GetExceptionMessage(e));
                //throw;
            }
            

            model.Req.Airport = db.Airport.Find(model.Req.AirportId);
            model.Req.Airport.AirportOffer = db.AirportOffers.Where(x => !x.IsDeleted).Where(x => x.AirportId == model.Req.AirportId).ToList();
            ViewBag.CustomerId = db.Customer.Where(c => c.UserId == user.Id).FirstOrDefault().Id;
            ViewBag.OfferFuelTypes = new SelectList(db.FuelType.ToList(), "Id", "Name");

            var AirportOffer = db.AirportOffers.Where(x => !x.IsDeleted).Where(x => x.AirportId == model.Req.AirportId).ToList();
            var offers = AirportOffer.Where(x => !x.Offer.IsDeleted)
                                                .Where(q => q.Offer.EndDate > DateTime.Now)
                                                .Select(q => new Select2ResultDTO() { id = q?.OfferId?.ToString(), text = q?.Offer?.FuelSupplier?.Name })
                                                .ToList();
            ViewBag.Offers = offers;

            return View(model);

            
        }




        [Authorize(Roles = "Customer")]
        // GET: Requests/Create
        public async Task<IActionResult> Pay(int? requestOfferId,long price, string priceUnit)
        {
            var user = (await GetCurrentUserAsync());
            var requestOffer = await db.RequestOffers
                .FirstOrDefaultAsync(m => m.Id == requestOfferId);
            if (requestOffer == null)
            {
                return NotFound();
            }

            try
            {
                requestOffer.RStatus = ReplyStatus.WaitingForPayment;
                db.Update(requestOffer);
                db.SaveChanges();

                #region Stripe
                // Set your secret key. Remember to switch to your live secret key in production!
                // See your keys here: https://dashboard.stripe.com/account/apikeys
                StripeConfiguration.ApiKey = StripeOptions?.Value?.SecretKey;

                var options = new PaymentIntentCreateOptions
                {
                    Amount = price * 100,
                    Currency = priceUnit,
                    // Verify your integration in this guide by including this parameter
                    Metadata = new Dictionary<string, string>()
                        {
                          {"integration_check", "accept_a_payment"},
                          {"request_payment", "request_payment"},
                          {"requestOfferId", ""+requestOfferId }
                        }
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);
                ViewBag.ClientSecret = paymentIntent.ClientSecret;
                
                #endregion
                ViewBag.RequestOfferId = requestOfferId;
                //ViewBag.ClientSecret = StripeOptions?.Value?.SecretKey;
                return PartialView("_Pay",new SimpleResponse(Constants.SUCCESS_CODE, Constants.SUCCESS));

            }
            catch (Exception e)
            {
                Serilog.Log.Error(e,Constants.PAYMENT_ERROR,$"Exeption:  {e}",   $"User : {User.Identity.Name}");
                Message = Toast.ErrorToastFront(GetExceptionMessage(e, "Payment Error. Contact site administrator."));
                //return Content(e.ToString());
            }

            ViewBag.RequestOfferId = requestOfferId;
            return PartialView("_Pay",new SimpleResponse(Constants.ERROR_CODE,Constants.ERROR));
            //RequestViewModel Request = new RequestViewModel();
        }


        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay(int? RequestOfferId)
        {
            try
            {
                var user = (await GetCurrentUserAsync());
                var requestOffer = await db.RequestOffers
                    .FirstOrDefaultAsync(m => m.Id == RequestOfferId);
                if (requestOffer == null)
                {
                    return NotFound();
                }

                var RequestOffer = db.RequestOffers.Find(RequestOfferId);
                RequestOffer.RStatus = ReplyStatus.Success;
                await db.SaveChangesAsync();
                Message = Toast.SucsessToastFront();
                return RedirectToAction("Details","Requests",new { id = requestOffer.RequestId});
            }
            catch (Exception e)
            {
                Message = Toast.ErrorToastFront(GetExceptionMessage(e));
                return await Pay(RequestOfferId);
            }

        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Customer")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task Pay(RequestViewModel model)    
        //{
        //    var user = (await GetCurrentUserAsync());

           

        //}


        private bool RequestExists(int id)
        {
            return db.Request.Any(e => e.Id == id);
        }
    }
}
