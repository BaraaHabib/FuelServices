using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Linq;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    public class CountriesController : BaseController
    {
        public CountriesController(AirportCoreContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        [AllowAnonymous]
        /// <summary>
        /// ajax action to get continent Countries
        /// </summary>
        /// <returns></returns>
        public IList GetCountries(int? ContinentId)
        {
            var Countries = db.Country.Where(e => e.ContinentId == ContinentId);
            return Countries.Select(x => new { x.Id, x.Name }).ToList();
        }
    }
}