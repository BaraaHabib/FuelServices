

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
using X.PagedList;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class AirportsController : BaseController
    {
        public AirportsController(IServiceProvider provider) : base(provider)
        {
        }

        //Filtering logic
        async System.Threading.Tasks.Task<IEnumerable<AirportModel>> FilterDataAsync(AirportFilterModel filterModel)
        {
            var res = await db.Airport.Where(x =>
            !x.IsDeleted &&
            (x.Name != null && x.Name.ToLower().Contains(filterModel.Term.ToLower())) ||
            (x.Icao != null && x.Icao.ToLower().Contains(filterModel.Term.ToLower())) ||
            (x.Iata != null && x.Iata.ToLower().Contains(filterModel.Term.ToLower()))
            )
             .Skip((filterModel.Page - 1) * filterModel.Limit)
                            .Take(filterModel.Limit)
                            .ToListAsync();

            var data = GetService<IMapper>().Map<List<Airport>, List<AirportModel>>(res);

            return data;
        }

        /// <summary>
        /// Get airports / autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        // GET: api/FuelSuppliers/5
        public async System.Threading.Tasks.Task<ActionResult<object>> Search([FromQuery] AirportFilterModel filter)
        {
            try
            {
                //Get the data for the current page
                var result = new PagedCollectionResponse<AirportModel>
                {
                    Items = await FilterDataAsync(filter)
                };

                //Get next page URL string
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                AirportFilterModel nextFilter = filter.Clone() as AirportFilterModel;
                nextFilter.Page += 1;

                var nextFilterItemsCount = (await FilterDataAsync(nextFilter)).Count();
                String nextUrl = nextFilterItemsCount <= 0 ? null : this.Url.Action(actionName, controllerName, nextFilter, Request.Scheme);

                //Get previous page URL string
                AirportFilterModel previousFilter = filter.Clone() as AirportFilterModel;
                previousFilter.Page -= 1;

                var prevFilterItemsCount = (await FilterDataAsync(previousFilter)).Count();
                String previousUrl = prevFilterItemsCount <= 0 ? null : this.Url.Action(actionName, controllerName, previousFilter, Request.Scheme);

                // when tying to take nigative pages
                previousUrl = (previousFilter.Page <= 0 && previousUrl != null) ? null : previousUrl;

                result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;
                result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;

                return new Response<PagedCollectionResponse<AirportModel>>(Constants.SUCCESS_CODE, result);
            }
            catch (Exception e)
            {
                return new Response<string>(Constants.SOMETHING_WRONG_CODE, GetExceptionMessage(e));
            }
        }
    }
}