using DBContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuelServices.ApplicationServices.BackgroundService
{
    class TimedHostedService
    {
        private Timer _timer;
        protected AirportCoreContext db;
        protected UserManager<ApplicationUser> UserManager;
        protected IServiceScopeFactory ServiceScopeFactory;
        private readonly IOptions<MyConfiguration> configuration;

        public TimedHostedService(
            //AirportCoreContext airportCoreContext,
            //UserManager<ApplicationUser> userManager
            IServiceScopeFactory serviceScopeFactory,
            IOptions<MyConfiguration> mconfig
            )
        {
            ServiceScopeFactory = serviceScopeFactory;
            configuration = mconfig;
            //UserManager = userManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Serilog.Log.Information("Timed Background Service is starting.", $"Time: {DateTime.UtcNow}");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(50));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                //var reqs = db.Request.Where(x => x.st)
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var db =
                        scope.ServiceProvider
                            .GetRequiredService<AirportCoreContext>();
                    var allRequests = db.RequestOffers.Where(x => x.RStatus == ReplyStatus.ApprovedBySupplier).ToList();
                    int TimeLimet = int.Parse(configuration.Value.CustomerConfirmationTimeOutInHours);

                    foreach (var req in allRequests)
                    {
                        if (DateTime.UtcNow > req.SupplierConfirmDate)
                        {
                            var elapsedTime = (DateTime.UtcNow - req.SupplierConfirmDate).Value.TotalHours;
                            if (elapsedTime > TimeLimet)
                            {
                                req.RStatus = ReplyStatus.Expired;
                                db.Update(req);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Information(e, "Exception in background Service", $"Message {e.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Serilog.Log.Information("Timed Background Service is stopping.", $"Time: {DateTime.UtcNow}");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
