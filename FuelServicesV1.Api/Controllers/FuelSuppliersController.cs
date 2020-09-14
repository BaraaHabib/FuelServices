using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Models;
using FuelServices.Api.Models.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class FuelSuppliersController : BaseController
    {
        public FuelSuppliersController(IServiceProvider provider) : base(provider)
        {
        }


        // GET: api/FuelSuppliers/5
        public async Task<ActionResult<object>> Search([FromQuery] OffersFilterModel filter)
        {
            try
            {
                //Filtering logic
                async Task<IEnumerable<OfferModel>> filterDataAsync(OffersFilterModel filterModel)
                {
                    var res = await db.AirportOffers.Where(x =>
                    x.Airport.Name.Contains(filterModel.Term.ToLower()) ||
                    x.Airport.Iata.Contains(filterModel.Term.ToLower()) ||
                    x.Airport.Icao.Contains(filterModel.Term.ToLower()))
                    //x.Airport.City.Name.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase) ||
                    //x.Airport.Country.Name.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase))
                    .Where(x => !x.IsDeleted && x.Offer.EndDate >= DateTime.Now)
                     .Skip((filterModel.Page - 1) * filter.Limit)
                                    .Take(filterModel.Limit)
                                    .Select(q => q.Offer)
                                    .ToListAsync();

                    var data = GetService<IMapper>().Map<List<Offer>, List<OfferModel>>(res);

                    for (int i = 0; i < res.Count; i++)
                    {
                        for (int j = 0; j < res[i].AirportOffers.Count; j++)
                        {
                            data[i].Airports[j].PriceForOffer = res[i].AirportOffers.ElementAtOrDefault(j)?.Price.ToString();
                            data[i].Airports[j].PriceUnitForOffer = res[i].AirportOffers.ElementAtOrDefault(j)?.PriceUnit.ToString();
                        }
                    }

                    return data;
                }

                //Get the data for the current page
                var result = new PagedCollectionResponse<OfferModel>
                {
                    Items = await filterDataAsync(filter)
                };

                //Get next page URL string
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                OffersFilterModel nextFilter = filter.Clone() as OffersFilterModel;
                nextFilter.Page += 1;

                var nextFilterItemsCount = (await filterDataAsync(nextFilter)).Count();
                String nextUrl = nextFilterItemsCount <= 0 ? null : this.Url.Action(actionName, controllerName, nextFilter, Request.Scheme);

                //Get previous page URL string
                OffersFilterModel previousFilter = filter.Clone() as OffersFilterModel;
                previousFilter.Page -= 1;

                var prevFilterItemsCount = (await filterDataAsync(previousFilter)).Count();
                String previousUrl = prevFilterItemsCount <= 0 ? null : this.Url.Action(actionName, controllerName, previousFilter, Request.Scheme);

                // when tying to take nigative pages
                previousUrl = (previousFilter.Page <= 0 && previousUrl != null) ? null : previousUrl;

                result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;
                result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;

                return new Response<PagedCollectionResponse<OfferModel>>(Constants.SUCCESS_CODE, result);
            }
            catch (Exception e)
            {
                return new Response<string>(Constants.SOMETHING_WRONG_CODE, GetExceptionMessage(e));
            }
        }

        public string Test()
        {
            return "test";
        }
    }
}