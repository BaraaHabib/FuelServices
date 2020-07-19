using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using FuelServices.Api.Models.Paging;
using FuelServices.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using FuelServices.Api.Helpers;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class FuelSuppliersController : BaseController
    {

        public FuelSuppliersController(IServiceProvider provider) : base(provider)
        {
        }

        // GET: api/FuelSuppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelSupplier>>> GetFuelSupplier()
        {
            return await db.FuelSupplier.ToListAsync();
        }

        // GET: api/FuelSuppliers/5
        public async Task<ActionResult<FuelSupplier>> GetFuelSupplier(int id)
        {
            var fuelSupplier = await db.FuelSupplier.FindAsync(id);

            if (fuelSupplier == null)
            {
                return NotFound();
            }

            return fuelSupplier;
        }

        // GET: api/FuelSuppliers/5
        public ActionResult<object> Search([FromQuery] OffersFilterModel filter)
        {
            try
            {
                //Filtering logic  
                Func<OffersFilterModel, IEnumerable<OfferModel>> filterData = (filterModel) =>
                {
                    var res = db.AirportOffers.Where(x =>
                    x.Airport.Name.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase) ||
                    x.Airport.Iata.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase) ||
                    x.Airport.Icao.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase) )
                    //x.Airport.City.Name.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase) ||
                    //x.Airport.Country.Name.Contains(filterModel.Term ?? String.Empty, StringComparison.OrdinalIgnoreCase))
                    .Where(x => !x.IsDeleted && x.Offer.EndDate >= DateTime.Now)
                     .Skip((filterModel.Page - 1) * filter.Limit)
                                    .Take(filterModel.Limit)
                                    .Select(q => q.Offer)
                                    .ToList();

                    var data = Mapper.Map<List<Offer>, List<OfferModel>>(res);

                    for (int i = 0; i < res.Count; i++)
                    {
                        for (int j = 0; j < res[i].AirportOffers.Count; j++)
                        {
                            data[i].Airports[j].PriceForOffer = res[i].AirportOffers.ElementAtOrDefault(j)?.Price.ToString();
                            data[i].Airports[j].PriceUnitForOffer = res[i].AirportOffers.ElementAtOrDefault(j)?.PriceUnit.ToString();
                        }
                    }


                    return data;

                };

                //Get the data for the current page  
                var result = new PagedCollectionResponse<OfferModel>();
                result.Items = filterData(filter);


                //Get next page URL string  
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                OffersFilterModel nextFilter = filter.Clone() as OffersFilterModel;
                nextFilter.Page += 1;

                var nextFilterItemsCount = filterData(nextFilter).Count();
                String nextUrl = nextFilterItemsCount <= 0 ? null : this.Url.Action(actionName, controllerName, nextFilter, Request.Scheme);


                //Get previous page URL string  
                OffersFilterModel previousFilter = filter.Clone() as OffersFilterModel;
                previousFilter.Page -= 1;

                var prevFilterItemsCount = filterData(previousFilter).Count();
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

        // PUT: api/FuelSuppliers/5
        public async Task<IActionResult> PutFuelSupplier(int id, FuelSupplier fuelSupplier)
        {
            if (id != fuelSupplier.Id)
            {
                return BadRequest();
            }

            db.Entry(fuelSupplier).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelSupplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FuelSuppliers
        [HttpPost]
        public async Task<ActionResult<FuelSupplier>> PostFuelSupplier(FuelSupplier fuelSupplier)
        {
            db.FuelSupplier.Add(fuelSupplier);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetFuelSupplier", new { id = fuelSupplier.Id }, fuelSupplier);
        }

        // DELETE: api/FuelSuppliers/5
        public async Task<ActionResult<FuelSupplier>> DeleteFuelSupplier(int id)
        {
            var fuelSupplier = await db.FuelSupplier.FindAsync(id);
            if (fuelSupplier == null)
            {
                return NotFound();
            }

            db.FuelSupplier.Remove(fuelSupplier);
            await db.SaveChangesAsync();

            return fuelSupplier;
        }

        private bool FuelSupplierExists(int id)
        {
            return db.FuelSupplier.Any(e => e.Id == id);
        }
    }
}
