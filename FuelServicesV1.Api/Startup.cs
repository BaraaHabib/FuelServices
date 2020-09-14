using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Helpers.Background_Service;
using FuelServices.Api.Helpers.Stripe;
using FuelServices.Api.Mapping;
using FuelServices.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Stripe;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FuelServices.Api
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
            
            services.AddCors();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

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
                  ));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<AirportCoreContext>();

            var appSettingsSection = Configuration.GetSection("MyConfiguration");
            services.Configure<MyConfiguration>(appSettingsSection);
            var appSettings = appSettingsSection.Get<MyConfiguration>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
                    .AddEntityFrameworkStores<AirportCoreContext>()
                    //.AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers().AddNewtonsoftJson();
            ///// configure jwt authentication

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });

            ////////
            ///
            //data mapper profiler setting

            //IMapper mapper = mappingConfig.CreateMapper();

            services.AddHostedService<TimedHostedService>();

            services.AddAutoMapper(opt => opt.AddProfile<MappingProfile>(), typeof(Startup));
            
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .Select(e =>
                                e.Value.Errors.First().ErrorMessage
                                //Error
                                //{
                                //    Name = e.Key,
                                //    Message = e.Value.Errors.First().ErrorMessage
                                //}
                                ).ToArray();
                     var response = new Response<object>(Constants.SOMETHING_WRONG_CODE, errors, "Validation Errors");
                     return new BadRequestObjectResult(response);
                };
            });

        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";


                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    // Use exceptionHandlerPathFeature to process the exception (for example, 
                    // logging), but do NOT expose sensitive error information directly to 
                    // the client.

                    object res = "";

                    if (env.IsDevelopment())
                    {
                        res = new Response<Exception>(Constants.SERVER_ERROR_CODE, exceptionHandlerPathFeature.Error, Constants.SERVER_ERROR);
                    }
                    else
                    {
                        res = new SimpleResponse(Constants.SERVER_ERROR_CODE, Constants.SERVER_ERROR);
                    }

                    var json = JsonConvert.SerializeObject(res);
                    await context.Response.WriteAsync(json); // IE padding
                });
            });

            app.UseSession();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();

            app.UseHttpsRedirection();
            //
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}