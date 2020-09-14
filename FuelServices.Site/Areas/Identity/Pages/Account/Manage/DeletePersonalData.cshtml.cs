using DBContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Site.Helpers;
using Site.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly AirportCoreContext _context;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            AirportCoreContext airportCoreContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = airportCoreContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Password not correct.");
                    return Page();
                }
            }
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        string UserEmail = user.Email;
                        var result = await _userManager.DeleteAsync(user);
                        var userId = await _userManager.GetUserIdAsync(user);
                        if (!result.Succeeded)
                        {
                            _logger.LogInformation(new InvalidOperationException($"Unexpected error " +
                                $"occurred deleteing user with ID '{userId}'."), "User with ID '{UserId}'" +
                                " deleted themselves.", userId);
                        }
                        var contentAppName = _context.ContentManagement.Where(cm => cm.Name == "app_name")
                                       .FirstOrDefault();
                        string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

                        EmailBodyDefaultParams emailBodyDefaultParams = _context.EmailBodyDefaultParams
                                .Where(e => e.EmailTypeName == "delete_account").FirstOrDefault();
                        string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                        EmailSender.SendEmail(UserEmail, AppName, body);

                        await _signInManager.SignOutAsync();
                        transaction.Commit();
                        _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            });
            var simpleResponse = new SimpleResponse(Constants.INFO_CODE, "Account deleted successfully");
            TempData.Set("Toast", simpleResponse);
            return Redirect("~/");
        }
    }
}