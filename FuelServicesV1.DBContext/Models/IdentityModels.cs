using FuelServices.DBContext.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBContext.Models
{
    public class ApplicationRole : IdentityRole
    {
       
        public ApplicationRole() : base()
        {

        }
       
        public ApplicationRole(string Name) : base(Name)
        {

        }

    }

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        { }

        public bool IsActive { get; set; } = true;

        public virtual FuelSupplier FuelSupplier { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual AdvertisementOwner AdvertisementOwner { get; set; }

        public virtual ICollection<ResetPasswordToken> ResetPasswordTokens { get; set; }

    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        //UserManager<ApplicationUser> UserManager;
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation) 
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            //UserManager = userManager;

        }


        public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (user != null)
                if (!user.IsActive)
                {
                    return Task.FromResult(SignInResult.NotAllowed);
                }
            return base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
                if (!user.IsActive)
                {
                    return Task.FromResult(SignInResult.NotAllowed);
                }
            return base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }

}
