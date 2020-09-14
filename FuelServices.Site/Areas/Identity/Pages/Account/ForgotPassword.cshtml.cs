using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Helpers;
using Site.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Site.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly AirportCoreContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, AirportCoreContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                EmailBodyDefaultParams emailBodyDefaultParams = _context.EmailBodyDefaultParams
                            .Where(e => e.EmailTypeName == "reset_password").FirstOrDefault();
                string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));

                var contentAppName = _context.ContentManagement.Where(cm => cm.Name == "app_name")
                               .FirstOrDefault();
                string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

                var simpleResponse = EmailSender.SendEmail(Input.Email, AppName, body);
                TempData.Set("Toast", simpleResponse);

                if (simpleResponse.Code != Constants.SUCCESS_CODE)
                {
                    ModelState.AddModelError(string.Empty, simpleResponse.Message);
                }
                Serilog.Log.Information(simpleResponse.Message, simpleResponse.Message, Input.Email);
                return LocalRedirect(Url.Content("~/"));
            }
            Serilog.Log.Error(Constants.SOMETHING_WRONG, Constants.SOMETHING_WRONG, Input.Email);
            return Page();
        }
    }
}