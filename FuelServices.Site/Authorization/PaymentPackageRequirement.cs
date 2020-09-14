using DBContext.Models;
using Microsoft.AspNetCore.Authorization;

namespace Site.Authorization
{
    public class PaymentPackageRequirement : IAuthorizationRequirement
    {
        public PaymentPackageRequirement(PaymentPackage paymentPackage)
        {
            PaymentPackage = paymentPackage;
        }

        public PaymentPackage PaymentPackage { get; private set; }
    }
}