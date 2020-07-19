using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.DTOs;
using X.PagedList;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    public class AirportsController : BaseController
    {
        public AirportsController(AirportCoreContext context, IServiceProvider provider) : base(context, provider)
        {
        }
  


        /// <summary>
        /// Get airports / autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public Select2DTO GetAirport(string q, int? page)
        {
            try
            {
                var query = db.Airport.AsQueryable();

                if (!String.IsNullOrWhiteSpace(q))
                    query = query.Where(e =>
                    e.Name.Contains(q) ||
                    e.Icao.Contains(q) ||
                    e.Iata.Contains(q));
                X.PagedList.IPagedList<Airport> pagedAirports = query.ToPagedList(page ?? 1, 30);
                List<Select2ResultDTO> result = new List<Select2ResultDTO>();
                foreach (var item in pagedAirports)
                {
                    Select2ResultDTO temp = new Select2ResultDTO
                    {
                        id = item.Id.ToString(),
                        text = $"{item.Name}"
                    };
                    result.Add(temp);
                }
                Select2PaginateDTO select2PaginateDTO = new Select2PaginateDTO
                {
                    more = pagedAirports.HasNextPage
                };
                Select2DTO select2DTO = new Select2DTO
                {
                    results = result,
                    paginate = select2PaginateDTO,
                };
                return select2DTO;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

    }
}