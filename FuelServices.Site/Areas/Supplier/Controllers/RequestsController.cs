using DBContext.Models;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    public class RequestsController : BaseController
    {
        public RequestsController(AirportCoreContext context, IServiceProvider provider) : base(context, provider)
        {
        }

        // GET: Supplier/Requests
        //public async Task<IActionResult> Index()
        //{
        //    var user = await GetCurrentUserAsync();
        //    var sup = db.FuelSupplier.Find(user.FuelSupplier);
        //    //db.RequestOffers.Where(w => w.Offer.FuelSupplierId == sup.Id).Where(q => q.Offer;
        //    var airportCoreContext = db.Request.Include(r => r.Airport).Include(r => r.Customer);
        //    return View(await airportCoreContext.ToListAsync());
        //}

        // GET: Supplier/Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestOffer = await db.RequestOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestOffer == null)
            {
                return NotFound();
            }

            return View(requestOffer);
        }

        /// <summary>
        /// get requests for offer
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OfferRequests(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var user = await GetCurrentUserAsync();
                var sup = db.FuelSupplier.Find(user.FuelSupplier.Id);
                var offer = db.Offer.Find(id);
                if (!sup.Offer.ToList().Contains(offer)) // if offer not for supplier
                    return BadRequest();
                offer.RequestOffers.ToList().ForEach(e =>
                {
                    e.Request = db.Request.Find(e.RequestId);
                });
                var requests = offer.RequestOffers.ToList();
                ViewBag.Offer = offer;
                return View(requests);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Requests.OfferRequests");
                return View(new List<RequestOffers>());
            }
        }

        [HttpPost, ActionName("RejectRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectRequest(int? id)
        {
            var requestOffer = await db.RequestOffers.FindAsync(id);
            if (requestOffer == null)
                return BadRequest();
            var user = await GetCurrentUserAsync();
            if (requestOffer.Offer.FuelSupplier.Id != user.FuelSupplier.Id)
            {
                return BadRequest();
            }
            requestOffer.RStatus = ReplyStatus.Rejected;
            db.RequestOffers.Update(requestOffer);
            await db.SaveChangesAsync();
            Message = Toast.SucsessToast();
            return RedirectToAction("Details", new { id });
        }

        [HttpPost, ActionName("ApproveRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int? id)
        {
            var requestOffer = await db.RequestOffers.FindAsync(id);
            if (requestOffer == null)
                return BadRequest();
            var user = await GetCurrentUserAsync();
            if (requestOffer.Offer.FuelSupplier.Id != user.FuelSupplier.Id)
            {
                return BadRequest();
            }
            requestOffer.RStatus = ReplyStatus.ApprovedBySupplier;
            requestOffer.SupplierConfirmDate = DateTime.UtcNow;
            db.RequestOffers.Update(requestOffer);
            await db.SaveChangesAsync();

            var request = requestOffer.Request;
            var otherRequests = request.RequestOffers.Where(x => x.Id != id);
            foreach (var item in otherRequests)
            {
                item.RStatus = ReplyStatus.AgreedWithASupplier;
            }
            db.UpdateRange(otherRequests);
            await db.SaveChangesAsync();
            Message = Toast.SucsessToast("Request approved, waiting for customer confirmation in 24 houres.");
            return RedirectToAction("Details", new { id });
        }

        private bool RequestExists(int id)
        {
            return db.Request.Any(e => e.Id == id);
        }
    }
}