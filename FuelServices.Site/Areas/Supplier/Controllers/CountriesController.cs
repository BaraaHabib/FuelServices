using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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