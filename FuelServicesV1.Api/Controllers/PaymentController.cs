using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Helpers.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class PaymentController : BaseController
    {
        protected readonly IOptions<StripeSettings> StripeOptions;

        public PaymentController(IServiceProvider serviceProvider, IOptions<StripeSettings> options) : base(serviceProvider)
        {
            this.StripeOptions = options;
        }

        [HttpGet]
        public async Task<object> Pay(int? requestOfferId, double amount)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var customer = user.Customer;

                var requestOffer = db.RequestOffers.Find(requestOfferId);
                if (requestOffer.IsDeleted 
                    || requestOffer.RStatus != ReplyStatus.WaitingForPayment 
                    || requestOffer.Request.CustomerId != customer.Id 
                    || requestOffer == null)
                    return new Response<string>(Constants.BAD_REQUEST_CODE, Constants.BAD_REQUEST);
                string userType = "";
                if (User.IsInRole("Customer"))
                {
                    userType = "Customer";
                }
                if (User.IsInRole("Supplier"))
                {
                    userType = "Supplier";
                }
                #region Stripe
                // Set your secret key. Remember to switch to your live secret key in production!
                // See your keys here: https://dashboard.stripe.com/account/apikeys
                StripeConfiguration.ApiKey = StripeOptions?.Value?.SecretKey;

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (int)(amount * 100),
                    Currency = "usd",
                    // Verify your integration in this guide by including this parameter
                    Metadata = new Dictionary<string, string>()
                        {
                          {"integration_check", "accept_a_payment"},
                          {"requestOfferId", requestOfferId.ToString()},
                          {"userId", user.Id},
                          {"userType", userType},
                        }
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);
                return new Response<object>(Constants.SUCCESS_CODE, paymentIntent.ClientSecret, Constants.SUCCESS);

                #endregion Stripe
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Helpers.Constants.PAYMENT_ERROR, $"User : {User.Identity.Name}");
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(e));
                //return Content(e.ToString());
            }
        }
    }
}
