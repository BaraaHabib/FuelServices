using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(AirportCoreContext airportCoreContext, IServiceProvider provider) : base(airportCoreContext, provider)
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var supplier = user.FuelSupplier;

                var offers = supplier.Offer.Where(x => !x.IsDeleted).Where(x => x.EndDate > DateTime.UtcNow);

                var ros = offers.Select(x => x.RequestOffers.ToList())
                    .Aggregate((x, y) =>
                    {
                        x.AddRange(y);
                        return x;
                    });

                var pendingRequests = ros.Where(x => x.RStatus == ReplyStatus.Pending)
                    .OrderByDescending(x => x.Request.SendDate)
                    .TakeLast(15).ToList();
                var CurrentlyProccessingRequests = ros.Where(x => CurrentlyProccessing(x.RStatus))
                    .OrderByDescending(x => x.Request.SendDate)
                    .TakeLast(15).ToList();

                ViewBag.PendingRequests = pendingRequests;

                ViewBag.CurrentlyProccessingRequests = CurrentlyProccessingRequests;
            }
            catch (Exception e)
            {
                //Message = Toast.ErrorToastFront();
                Serilog.Log.Error(e, "HomeController.Index");
            }

            return View();
        }

        private bool CurrentlyProccessing(ReplyStatus r)
        {
            switch (r)
            {
                case ReplyStatus.Success:
                case ReplyStatus.Pending:
                case ReplyStatus.Rejected:
                case ReplyStatus.Expired:
                    return false;

                case ReplyStatus.ApprovedBySupplier:
                case ReplyStatus.ConfirmedByCustomer:
                case ReplyStatus.AgreedWithASupplier:
                case ReplyStatus.WaitingForPayment:
                    return true;
            }

            return false;
        }
    }
}