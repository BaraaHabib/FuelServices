using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Site.Areas.Identity.IdentityHostingStartup))]

namespace Site.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}