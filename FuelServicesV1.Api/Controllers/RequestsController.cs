using AutoMapper;
using Castle.DynamicProxy.Generators;
using DBContext.Models;
using Elect.Web.IUrlHelperUtils;
using FuelServices.Api.Helpers;
using FuelServices.Api.Helpers.Attributes;
using FuelServices.Api.Models;
using FuelServices.Api.Models.Requests;
using FuelServices.Api.Models.SupplierContacts;
using FuelServices.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Customer")]
    [ValidateModel]
    public class RequestsController : BaseController
    {

        public readonly IWebHostEnvironment _env;
        public RequestsController(IServiceProvider serviceProvider, IWebHostEnvironment env) : base(serviceProvider)
        {
            _env = env;
        }

        // POST: api/Identity
        [HttpPost]
        //[Authorize(Roles ="Customer")]
        public async Task<object> Create([FromBody] RequestModel model)
        {
            var user = (await GetCurrentUserAsync());
            try
            {
                if (user.Customer == null)
                {
                    return new Response<bool>(Constants.Access_Denied_CODE, false, Constants.Access_Denied);
                }
                model.CustomerId = user.Customer?.Id;

                var mappingSrevice = GetService<IMapper>();
                Request request = mappingSrevice.Map<RequestModel, Request>(model);

                request.SendDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    EmailBodyDefaultParams emailBodyDefaultParams = db.EmailBodyDefaultParams
                        .Where(e => e.EmailTypeName == "supplier_request_notification").FirstOrDefault();
                    string body = GetService<IEmailSender>().CreateEmailBody(emailBodyDefaultParams);
                    //

                    db.Request.Add(request);
                    await db.SaveChangesAsync();

                    var AirportOffer = db.AirportOffers
                        .FirstOrDefault(x => x.OfferId == model.OfferId && x.AirportId == model.AirportId);
                    int? AirportOfferId = AirportOffer?.Id;

                    // get offer
                    var selectedOffer = db.Offer.Where(o => !o.IsDeleted).Where(o => o.Id == model.OfferId).FirstOrDefault();

                    if (AirportOffer == null || selectedOffer == null)
                    {
                        return new Response<bool>(Constants.BAD_REQUEST_CODE, false, Constants.BAD_REQUEST);
                    }

                    RequestOffers requestOffers = new RequestOffers()
                    {
                        OfferId = model.OfferId,
                        RequestId = request.Id,
                        AirportOfferId = AirportOfferId,
                        RStatus = ReplyStatus.Pending,
                    };
                    db.Add(requestOffers);

                    await db.SaveChangesAsync();

                    #region send email

                    try
                    {
                        var supplierEmail = selectedOffer.FuelSupplier.User.Email;
                        var callbackUrl = Url.AbsoluteAction("Details", "Requests", new { Area = "Supplier", id = model.Id });
                        body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));
                        var simpleResponse = GetService<IEmailSender>().SendEmailNotification(supplierEmail, GetAppName(), body);
                    }
                    catch (Exception ex)
                    {
                        Serilog.Log.Logger.Error(ex, Constants.EMAIL_FAILED_TO_DELIVER, "api", "Controller : Requests", "Action: Create");
                    }

                    #endregion send email

                    model.Customer = $"{user.Customer.FirstName} {user.Customer.LastName}";
                    model.Airport = db.Airport.Find(model.AirportId)?.Name;
                    model.RequestOffers = mappingSrevice.Map<List<RequestOfferModel>>(request.RequestOffers);
                    model.Status = ReplyStatus.Pending.ToString();
                    model.Price = "" + Double.Parse(model.Quantity) * AirportOffer.Price;
                    model.PriceUnit = AirportOffer.PriceUnit;
                    model.RequestDate = request.SendDate;
                    model.Id = request.Id;
                    return new Response<RequestModel>(Constants.SUCCESS_CODE, model);
                }
                else
                {
                    return new Response<bool>(Constants.INVALID_INPUT_CODE, false, Constants.INVALID_INPUT);
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Logger.Error(e, Constants.EMAIL_FAILED_TO_DELIVER, "api", "Controller : Requests", "Action: Create");
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(e));
            }
        }


        [HttpPost]
        public async Task<object> ConfirmRequest([FromBody]int? requestOfferId)
        {
            var user = await GetCurrentUserAsync();
            var requestOffer = await db.RequestOffers.FindAsync(requestOfferId);

            if (requestOffer == null)
                return new SimpleResponse(Constants.NOT_FOUND_CODE, Constants.NOT_FOUND);

            if (requestOffer.RStatus != ReplyStatus.ApprovedBySupplier)
                return new SimpleResponse(Constants.BAD_REQUEST_CODE, Constants.BAD_REQUEST);

            if(requestOffer.Request.CustomerId != user.Customer.Id)
                return new SimpleResponse(Constants.BAD_REQUEST_CODE, Constants.BAD_REQUEST);

            requestOffer.RStatus
                = ReplyStatus.ConfirmedByCustomer;

            db.Update(requestOffer);
            await db.SaveChangesAsync();
            var result = GetService<IMapper>().Map<Request, RequestModel>(requestOffer.Request);

            return new Response<RequestModel>(Constants.SUCCESS_CODE, result, Constants.SUCCESS);

        }

        [HttpPost]
        public async Task<object> CompleteRequestAndGetSupplierContacts([FromBody] int? requestOfferId)
        {
            var requestOffer = db.RequestOffers.Find(requestOfferId);
            
            if (requestOffer == null)
                return new SimpleResponse(Constants.NOT_FOUND_CODE, Constants.NOT_FOUND);

            if (requestOffer.RStatus != ReplyStatus.ConfirmedByCustomer)
                return new SimpleResponse(Constants.BAD_REQUEST_CODE, Constants.BAD_REQUEST);

            requestOffer.RStatus = ReplyStatus.Success;
            db.Update(requestOffer);
            await db.SaveChangesAsync();
            SupplierContactsAPIResult supplierContactsAPIResult = GetRequestSupplierContacts(requestOfferId);

            return new Response<SupplierContactsAPIResult>(Constants.SUCCESS_CODE, supplierContactsAPIResult, Constants.SUCCESS);

        }

        [HttpPost]
        public async Task<object> StartPayment([FromBody] int? requestOfferId)
        {
            var requestOffer = db.RequestOffers.Find(requestOfferId);

            if (requestOffer == null)
                return new SimpleResponse(Constants.NOT_FOUND_CODE, Constants.NOT_FOUND);

            if (requestOffer.RStatus != ReplyStatus.ConfirmedByCustomer)
                return new SimpleResponse(Constants.BAD_REQUEST_CODE, Constants.BAD_REQUEST);

            requestOffer.RStatus = ReplyStatus.WaitingForPayment;
            db.Update(requestOffer);
            await db.SaveChangesAsync();

            return new SimpleResponse(Constants.SUCCESS_CODE, Constants.SUCCESS);

        }


        [HttpGet]
        public object RequestSupplierContacts(int? requestOfferId)
        {
            var requestOffer = db.RequestOffers.Find(requestOfferId);

            if (requestOffer == null)
                return new SimpleResponse(Constants.NOT_FOUND_CODE, Constants.NOT_FOUND);

            if (requestOffer.RStatus != ReplyStatus.Success)
                return new SimpleResponse(Constants.BAD_REQUEST_CODE, Constants.BAD_REQUEST);

            SupplierContactsAPIResult supplierContactsAPIResult = GetRequestSupplierContacts(requestOfferId);

            return new Response<SupplierContactsAPIResult>(Constants.SUCCESS_CODE, supplierContactsAPIResult, Constants.SUCCESS);


        }

        //[HttpGet]
        //public object GetRequests( int? OfferId)
        //{
        //    try
        //    {
        //        if (!User.IsInRole("Customer"))
        //        {
        //            return new Response<bool>(Constants.Access_Denied_CODE, false, Constants.Access_Denied);
        //        }

        //        var offer = db.Offer.FirstOrDefault(x => !x.IsDeleted
        //       //&& x.Status == OfferStatus.Active.ToString()
        //       //&& x.EndDate > DateTime.Now
        //       && x.Id == OfferId);
        //        if (offer == null)
        //        {
        //            return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
        //        }

        //        var result = GetService<IMapper>().Map<List<Request>, List<RequestModel>>(offer.RequestOffers.Select(x => x.Request).ToList());
        //        result.ForEach(x =>
        //       {
        //           var req = db.Request.Find(x.Id);

        //           x.Supplier = offer.FuelSupplier.Name;
        //           x.OfferId = OfferId;
        //           x.Price = "" + req.RequestOffers.FirstOrDefault()?.AirportOffer.Price * double.Parse(x.Quantity);
        //           x.PriceUnit = req.RequestOffers.FirstOrDefault()?.AirportOffer.PriceUnit;
        //       }
        //        );
        //        return new Response<List<RequestModel>>(Constants.SUCCESS_CODE, result, Constants.SUCCESS);
        //    }
        //    catch (Exception e)
        //    {
        //        return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(e));
        //    }
        //}

        [HttpGet]
        public async Task<object> GetUserRequests()
        {
            try
            {
                if (!User.IsInRole("Customer"))
                {
                    return new Response<bool>(Constants.Access_Denied_CODE, false, Constants.Access_Denied);
                }
                var user = await GetCurrentUserAsync();
                var mappingSrevice = GetService<IMapper>();

                var reqs = db.Request
                    .Where(x => x.Customer.Id == user.Customer.Id && !x.IsDeleted)
                    .ToList();
                var result = mappingSrevice.Map<List<RequestModel>>(reqs);
                for (int i = 0; i < reqs.Count; i++)
                {
                    var req = reqs[i];

                    var oid = req.RequestOffers.FirstOrDefault()?.OfferId;
                    var offer = await db.Offer.FindAsync(oid);
                    result[i].RequestOffers = mappingSrevice.Map<List<RequestOfferModel>>(req.RequestOffers.ToList());
                    result[i].OfferId = oid;

                    result[i].Price = "" + req.RequestOffers.FirstOrDefault()?.AirportOffer.Price * double.Parse(result[i].Quantity);
                    result[i].PriceUnit = req.RequestOffers.FirstOrDefault()?.AirportOffer.PriceUnit;
                }
                return new Response<List<RequestModel>>(Constants.SUCCESS_CODE, result, Constants.SUCCESS);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Constants.LogTemplates.REQUESTS_ERROR_EX);
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(e));
            }
        }

        public void Remove()
        {
            var c = db.Request.Where(x => x.RequestOffers.Count == 0);
            db.RemoveRange(c);
            db.SaveChanges();
        }
        private SupplierContactsAPIResult GetRequestSupplierContacts(int? requestOfferId)
        {
            var requestOffer = db.RequestOffers.Find(requestOfferId);

            var globalContacts = requestOffer.Offer.FuelSupplier.SupplierContact.ToList();

            var personsContacts = requestOffer.Offer.FuelSupplier.SupplierContactPerson.ToList();

            var mapperService = GetService<IMapper>();

            SupplierContactsAPIResult supplierContactsAPIResult = new SupplierContactsAPIResult()
            {
                GlobalContacts = mapperService.Map<List<GlobalContact>>(globalContacts),
                PersonContacts = mapperService.Map<List<PersonContact>>(personsContacts),
                Supplier = requestOffer.Offer.FuelSupplier.Name
            };

            return supplierContactsAPIResult;

        }

    }
}