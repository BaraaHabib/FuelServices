using Site.DTOs;
using Site.Helpers;
using Site.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using X.PagedList;

namespace Site.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        private readonly AirportCoreContext _context;

        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            AirportCoreContext context,
            IFileProvider fileProvider,
            IHostingEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            _fileProvider = fileProvider;
            _hostingEnvironment = env;

        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Profile Picture")]
            public string ImageUrl { get; set; }

            [Display(Name = "Country")]
            public int? CountryId { get; set; }

            public string CountryName { get; set; }

            public IFormFile file { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Customer customer = _context.Customer.Include(c => c.Country).Where(c => c.UserId == user.Id).FirstOrDefault();
            if (customer != null)
            {
                Input = new InputModel
                {
                    Email = email,
                    PhoneNumber = phoneNumber,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ImageUrl = customer.ImageUrl,
                    CountryId = customer.CountryId,
                    CountryName = customer.Country != null ? customer.Country.Name : ""
                };
            }
            else
            {
                Input = new InputModel
                {
                    Email = email,
                    PhoneNumber = phoneNumber
                };
            }

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            Customer customer = _context.Customer.Where(c => c.UserId == user.Id).FirstOrDefault();
            if (customer != null)
            {
                if (Input.file != null)
                {
                    FileInfo fi = new FileInfo(Input.file.FileName);
                    var newFilename = "P" + customer.Id + "_" + string.Format("{0:d}",
                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    var webPath = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\uploads\customers\" + newFilename);

                    var pathToSave = @"/uploads/customers/" + newFilename;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Input.file.CopyToAsync(stream);
                    }
                    customer.ImageUrl = pathToSave;
                }

                customer.FirstName = Input.FirstName;
                customer.LastName = Input.LastName;
                customer.CountryId = Input.CountryId;

                _context.Update(customer);
                _context.SaveChanges();
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                Serilog.Log.Error("Model state is not valid", "Model state is not valid", _userManager.GetUserId(User));
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                Serilog.Log.Error($"Unable to load user with ID '{_userManager.GetUserId(User)}'.",
                    $"Unable to load user with ID '{_userManager.GetUserId(User)}'.", _userManager.GetUserId(User));
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var contentAppName = _context.ContentManagement.Where(cm => cm.Name == "app_name")                          
                .FirstOrDefault();
            string  AppName  = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);

            EmailBodyDefaultParams emailBodyDefaultParams = _context.EmailBodyDefaultParams
                            .Where(e => e.EmailTypeName == "confirm_mail").FirstOrDefault();
            string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
            body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));

            SimpleResponse jsonResult = EmailSender.SendEmail(Input.Email, AppName, body);

            StatusMessage = jsonResult.Message;
            Serilog.Log.Information(jsonResult.Message, jsonResult.Message, user.Email);
            return RedirectToPage();
        }

        public JsonResult OnGetListAsync(string q, int? page)
        {
            var query = (from a in _context.Country
                         select a
                );

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(u => u.Name.Contains(q));
            }

            X.PagedList.IPagedList<Country> pagedCountries = query.ToPagedList(page ?? 1, 30);
            List<Select2ResultDTO> result = new List<Select2ResultDTO>();
            foreach (var item in pagedCountries)
            {
                Select2ResultDTO temp = new Select2ResultDTO();
                temp.id = item.Id.ToString();
                temp.text = item.Name;
                result.Add(temp);
            }
            Select2PaginateDTO select2PaginateDTO = new Select2PaginateDTO();
            select2PaginateDTO.more = pagedCountries.HasNextPage;
            Select2DTO select2DTO = new Select2DTO();
            select2DTO.results = result;
            select2DTO.paginate = select2PaginateDTO;
            return new JsonResult(select2DTO);
        }
    }
}
