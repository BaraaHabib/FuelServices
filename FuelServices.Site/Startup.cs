using AutoMapper;
using DBContext.Mapping;
using DBContext.Models;
using Elect.Web.DataTable;
using FuelServices.Site.Helpers.Background_Service;
using FuelServices.Site.Helpers.Configurations;
using FuelServices.Site.Helpers.Stripe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Site.Services;
using Stripe;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var appSettingsSection = Configuration.GetSection("MyConfiguration");
            services.Configure<MyConfiguration>(appSettingsSection);

            //services.Configure<MyConfiguration>(Configuration.GetSection("MyConfiguration"));

            services.AddDbContext<AirportCoreContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    }
                    ))
                ;

            services.AddScoped<AirportCoreContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
                .AddSignInManager<ApplicationSignInManager>()
                    .AddEntityFrameworkStores<AirportCoreContext>()

                    .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                //facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                //facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                facebookOptions.AppId = "733364797185856";
                facebookOptions.AppSecret = "43534b362a49644c4c64c4846c4c45cc";
            });

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                //microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                //microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];

                microsoftOptions.ClientId = "bcf190dc-c224-4590-8a61-b2f6f6fbecb2";
                microsoftOptions.ClientSecret = "2e6tQE-AG9?Ft:-DE3.Po4yPc3XjtR7z";
            });

            /////

            //services.AddScoped<IAuthorizationHandler, PropertyPackageAuthorizationHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddRazorPagesOptions(options =>
                {
                    //options.AllowAreas = true; frpm 2.2
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });
            services.AddMvc().AddViewOptions(options =>
            {
                //options.SuppressTempDataAttributePrefix = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
                .AddSessionStateTempDataProvider();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddSession();

            services.AddTransient<IEmailSender, EmailSender>();

            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardedHeaders =
            //        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            //});

            services.AddElectDataTable();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"))
            );

            services.AddHostedService<TimedHostedService>();

            // Auto Mapper Configurations
            //IMapper mapper = mappingConfig.CreateMapper();
            services.AddAutoMapper(opt =>
            {
                opt.Advanced.AllowAdditiveTypeMapCreation = true;
                opt.AddProfile<MappingProfile>();
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider services)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            if (env.ApplicationName == Environments.Development)
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();  from 2.2
                //app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapRazorPages();

                routes.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapAreaControllerRoute(
                name: "admin",
                areaName: "admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );

                routes.MapAreaControllerRoute(
               name: "supplier",
               areaName: "supplier",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );


            });
            CreateUserAndClaim(services).Wait();
        }

        private async Task CreateUserAndClaim(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            //Added Roles
            var roleResult = await RoleManager.FindByNameAsync("Admin");
            if (roleResult == null)
            {
                roleResult = new ApplicationRole("Admin");
                await RoleManager.CreateAsync(roleResult);
            }

            var customerRoleResult = await RoleManager.FindByNameAsync("Customer");
            if (customerRoleResult == null)
            {
                customerRoleResult = new ApplicationRole("Customer");
                await RoleManager.CreateAsync(customerRoleResult);
            }

            var supplierRoleResult = await RoleManager.FindByNameAsync("Supplier");
            if (supplierRoleResult == null)
            {
                supplierRoleResult = new ApplicationRole("Supplier");
                await RoleManager.CreateAsync(supplierRoleResult);
            }

            var roleClaimList = (await RoleManager.GetClaimsAsync(roleResult)).Select(p => p.Type);
            if (!roleClaimList.Contains("ManagerPermissions"))
            {
                await RoleManager.AddClaimAsync(roleResult, new Claim("ManagerPermissions", "true"));
            }

            ApplicationUser user = await UserManager.FindByEmailAsync("admin@admin.net");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin@admin.net",
                    Email = "admin@admin.net",
                    EmailConfirmed = true
                };
                await UserManager.CreateAsync(user, "123456");
            }

            await UserManager.AddToRoleAsync(user, "Admin");

            var claimList = (await UserManager.GetClaimsAsync(user)).Select(p => p.Type);
            if (!claimList.Contains("DateOfJoining"))
            {
                await UserManager.AddClaimAsync(user, new Claim("DateOfJoining", DateTime.UtcNow.ToShortDateString()));
            }
            if (!claimList.Contains("IsAdmin"))
            {
                await UserManager.AddClaimAsync(user, new Claim("IsAdmin", "true"));
            }
        }
    }
}