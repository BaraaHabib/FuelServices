using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Helpers.DTOs;
using FuelServices.Api.Models;
using FuelServices.Api.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class AirportsController : BaseController
    {
        public AirportsController(IServiceProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Get airports / autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>

        // GET: api/FuelSuppliers/5
        public ActionResult<object> Search([FromQuery] AirportFilterModel filter)
        {
            try
            {
                //Filtering logic  
                Func<AirportFilterModel, IEnumerable<AirportModel>> filterData = (filterModel) =>
                {
                    var res = db.Airport.Where(x =>
                    x.Name != null && x.Name.Contains(filter.Term,StringComparison.OrdinalIgnoreCase)  ||
                    x.Icao != null && x.Icao.Contains(filter.Term, StringComparison.OrdinalIgnoreCase) ||
                    x.Iata != null && x.Iata.Contains(filter.Term, StringComparison.OrdinalIgnoreCase))
                     .Skip((filterModel.Page - 1) * filter.Limit)
                                    .Take(filterModel.Limit)
                                    .ToList();

                    var data = Mapper.Map<List<Airport>, List<AirportModel>>(res);

                   
                    return data;

                };

                //Get the data for the current page  
                var result = new PagedCollectionResponse<AirportModel>();
                result.Items = filterData(filter);


                //Get next page URL string  
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                AirportFilterModel nextFilter = filter.Clone() as AirportFilterModel;
                nextFilter.Page += 1;

                var nextFilterItemsCount = filterData(nextFilter).Count();
                String nextUrl = nextFilterItemsCount <= 0 ? null : this.Url.Action(actionName, controllerName, nextFilter, Request.Scheme);


                //Get previous page URL string  
                AirportFilterModel previousFilter = filter.Clone() as AirportFilterModel;
                previousFilter.Page -= 1;

                var prevFilterItemsCount = filterData(previousFilter).Count();
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