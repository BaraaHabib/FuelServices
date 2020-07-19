using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using Site.Controllers;
using System.Linq.Dynamic.Core;
using FuelServices.DBContext.DatatablesModels;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FuelServices.Site.Controllers
{
    public class SuppliersController : BaseController
    {
        public SuppliersController(AirportCoreContext _db, IServiceProvider provider) : base(_db)
        {

        }

        [HttpPost]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var airportId = Request.Form["airportId"].FirstOrDefault();
                if (airportId == "")
                {
                    return new JsonResult("");
                }

                //var user = await GetCurrentUserAsync();
                var airport = await db.Airport.FindAsync(int.Parse(airportId));
                airport.AirportOffer = db.AirportOffers.Where(x => !x.IsDeleted).Where(x => x.AirportId == airport.Id).ToList();
                var tableData = airport.AirportOffer.Where(x => !x.Offer.IsDeleted)
                                                    .Where(q => q.Offer.EndDate > DateTime.Now)
                                                    .AsQueryable();

                //(from temp in db.Offer select temp);

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    tableData = tableData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    tableData = tableData.Where(d => !d.IsDeleted).Where(m => m.StartDate.ToString().Contains(searchValue) ||
                //    m.EndDate.ToString().Contains(searchValue) || m.Status.Contains(searchValue)
                //    || m.DuesTaxesLevies.Contains(searchValue));
                //}
                recordsTotal = tableData.Count();
                var data = tableData.Skip(skip).Take(pageSize).ToList();
                var res = Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = data.Select(x => new
                    {   
                        Id = x.OfferId,
                        Supplier = x.Offer.FuelSupplier.Name,
                        Price = x.Price + " " + x.PriceUnit,
                        EndDate = x.Offer.EndDate.ToShortDateString()
                    }).ToList(),
                }) ;
                return res;
            }
            catch (Exception e)
            {
                return new JsonResult(new object());
            }
        }

        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Contacts(int? RequestOfferId,int? supplierId)
        {
            try
            {
                var supplier = await db.FuelSupplier.FindAsync(supplierId);
                if (supplier == null)
                {
                    return NotFound();
                }

                var RequestOffer = db.RequestOffers.Find(RequestOfferId);
                if (RequestOffer == null)
                {
                    return NotFound();
                }

                RequestOffer.RStatus = ReplyStatus.Success;
                await db.SaveChangesAsync();

                var res = supplier.SupplierContactPerson.ToList();
                ViewBag.Contacts = supplier.SupplierContact.ToList();
                return View(res);
            }

            catch (Exception e)
            {
                Message = Toast.ErrorToastFront(GetExceptionMessage(e));
                return View(new List<SupplierContactPerson>());
            }
        }
    }
}