using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Authorization
{
    public class PropertyPackageAuthorizationHandler
                : AuthorizationHandler<PaymentPackageRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyPackageAuthorizationHandler(IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                   PaymentPackageRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            else
            {
                var user = _userManager.GetUserAsync(context.User);
                var claims = _userManager.GetClaimsAsync(user.Result);

                if (claims.Result.Where(c => c.Type == "PaymentPackageId").FirstOrDefault() == null)
                {
                    return Task.CompletedTask;
                }
                else
                {
                    var packageSubscriptionDate = Convert.ToDateTime(claims.Result
                        .Where(c => c.Type == "PackageExpiryDate").FirstOrDefault().Value);
                    var paymentPackageId = Convert.ToInt32(claims.Result
                        .Where(c => c.Type == "PaymentPackageId").FirstOrDefault().Value);
                    var paymentPackageLevel = Convert.ToInt32(claims.Result
                        .Where(c => c.Type == "PaymentPackageLevel").FirstOrDefault().Value);
                    var PaymentPackageNumberOfRequests = Convert.ToInt32(claims.Result
                        .Where(c => c.Type == "PaymentPackageNumberOfRequests").FirstOrDefault().Value);

                    if (packageSubscriptionDate != null)
                    {
                        double calculatedTimeLeft = (packageSubscriptionDate.Date - DateTime.Now.Date).TotalSeconds;
                        if (calculatedTimeLeft <= 0)
                        {
                            return Task.CompletedTask;
                        }

                        if (PaymentPackageNumberOfRequests <= 0)
                        {
                            return Task.CompletedTask;
                        }

                        if (context.User.IsInRole("Supplier"))
                        {
                            if (requirement.PaymentPackage.Type != PackageType.SupplierPackage)
                                return Task.CompletedTask;
                        }
                        else
                        {
                            if (requirement.PaymentPackage.Type != PackageType.CustomerPackage)
                                return Task.CompletedTask;
                        }

                        if (paymentPackageId == requirement.PaymentPackage.Id)
                        {
                            context.Succeed(requirement);
                        }
                        else if (paymentPackageLevel > requirement.PaymentPackage.ItemLevel)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}