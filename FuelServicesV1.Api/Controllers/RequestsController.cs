using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class RequestsController : BaseController
    {
        public RequestsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        // POST: api/Identity
        [HttpPost]
        //[Authorize(Roles ="Customer")]
        [Authorize(Roles = "Customer")]
        public async Task<object> Create([FromBody] RequestModel model)
        {

            var user = (await GetCurrentUserAsync());
            try
            {
                if(user.Customer == null)
                {
                    return new Response<bool>(Constants.Access_Denied_CODE, false, Constants.Access_Denied);
                }
                model.CustomerId = user.Customer?.Id;


                Request request = Mapper.Map<RequestModel, Request>(model);
                
                request.SendDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Request.Add(request);
                    var AirportOffer = db.AirportOffers
                        .FirstOrDefault(x => x.OfferId == model.OfferId && x.AirportId == model.AirportId);
                    int? AirportOfferId = AirportOffer?.Id;
                    if (AirportOffer == null)
                    {
                        return new Response<bool>(Constants.BAD_REQUEST_CODE,false, Constants.BAD_REQUEST);
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

                    model.Customer = $"{user.Customer.FirstName} {user.Customer.LastName}";
                    model.Airport = db.Airport.Find(model.AirportId)?.Name;
                    model.Supplier = db.Offer.Find(model.OfferId)?.FuelSupplier?.Name;
                    model.Status = ReplyStatus.Pending.ToString();
                    model.Price = ""+Double.Parse(model.Quantity) * AirportOffer.Price;
                    model.PriceUnit =  AirportOffer.PriceUnit;
                    model.requestDate = request.SendDate;
                    model.Id = request.Id;
                    return new Response<RequestModel>(Constants.SUCCESS_CODE, model);
                }
                else
                {
                    return new Response<bool>(Constants.INVALID_INPUT_CODE,false, Constants.INVALID_INPUT);
                }
            }
            catch (Exception e)
            {
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE,false, GetExceptionMessage(e));
            }


        }

        [Authorize(Roles ="Customer")]
        [HttpGet]
        public object GetRequests(int? OfferId )
        {

            try
            {
                if (!User.IsInRole("Customer"))
                {
                    return new Response<bool>(Constants.Access_Denied_CODE, false, Constants.Access_Denied);
                }

                    var offer = db.Offer.FirstOrDefault(x => !x.IsDeleted
                   //&& x.Status == OfferStatus.Active.ToString() 
                   //&& x.EndDate > DateTime.Now
                   && x.Id == OfferId);
                    if (offer == null)
                    {
                        return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                    }

                   var result = Mapper.Map<List<Request>, List<RequestModel>>(offer.RequestOffers.Select(x => x.Request).ToList());
                result.ForEach( x =>
                {
                    var req = db.Request.Find(x.Id);

                    x.Supplier = offer.FuelSupplier.Name;
                    x.OfferId = OfferId;
                    x.Price = "" + req.RequestOffers.FirstOrDefault()?.AirportOffer.Price * double.Parse(x.Quantity);
                    x.PriceUnit = req.RequestOffers.FirstOrDefault()?.AirportOffer.PriceUnit;
                }
                );
                return new Response<List<RequestModel>>(Constants.SUCCESS_CODE, result, Constants.SUCCESS);

            }
            catch (Exception e)
            {
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE,false,GetExceptionMessage(e));
            }

        }
        
        [Authorize(Roles ="Customer")]
        [HttpGet]
        public async Task<object> GetUserRequests( )
        {

            try
            {
                if (!User.IsInRole("Customer"))
                {
                    return new Response<bool>(Constants.Access_Denied_CODE, false, Constants.Access_Denied);
                }
                var user = await GetCurrentUserAsync();
                var reqs = db.Request
                    .Where(x => x.Customer.Id == user.Customer.Id)
                    .ToList();
                var result = Mapper.Map<List<Request>, List<RequestModel>>(reqs);
                for (int i = 0; i < reqs.Count; i++)
                {
                    var req = reqs[i];
                    
                    var oid = req.RequestOffers.FirstOrDefault()?.OfferId;
                    var offer = await db.Offer.FindAsync(oid);
                    result[i].Supplier = offer.FuelSupplier.Name;
                    result[i].OfferId = oid;

                    result[i].Price = "" + req.RequestOffers.FirstOrDefault()?.AirportOffer.Price * double.Parse(result[i].Quantity);
                    result[i].PriceUnit = req.RequestOffers.FirstOrDefault()?.AirportOffer.PriceUnit;
                }
                return new Response<List<RequestModel>>(Constants.SUCCESS_CODE, result, Constants.SUCCESS);

            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Constants.LogTemplates.REQUESTS_ERROR_EX);
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE,false,GetExceptionMessage(e));
            }

        }

    }
}