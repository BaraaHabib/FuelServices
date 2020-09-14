using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Linq;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    public class CitiesController : BaseController
    {
        public CitiesController(AirportCoreContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        [AllowAnonymous]
        /// <summary>
        /// ajax action to get Country Cities
        /// </summary>
        /// <returns></returns>
        public IList GetCities(int? ContinentId, int? CountryId)
        {
            var c = db.City.Where(e => e.CountryId == CountryId && e.Country.ContinentId == ContinentId);
            return c.Select(x => new { x.Id, x.Name }).ToList();
        }
    }
}