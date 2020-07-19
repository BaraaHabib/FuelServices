using Site.DTOs;
using Site.Helpers;
using Site.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using X.PagedList;

namespace Site.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SupplierRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly AirportCoreContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SupplierRegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            AirportCoreContext context,
            IServiceProvider serviceProvider,
            IFileProvider fileProvider,
            IHostingEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _serviceProvider = serviceProvider;
            _fileProvider = fileProvider;
            _hostingEnvironment = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }

            public string Position { get; set; }

            [Required]
            [Display(Name = "Company Description")]
            public string CompanyDescription { get; set; }

            [Required]
            [Display(Name = "Company Web Site")]
            public string CompanyWebSite { get; set; }

            public int? CountryId { get; set; }

            [Display(Name = "Country")]
            public string CountryName { get; set; }

            [Display(Name = "Is Middler")]
            public bool? IsMiddler { get; set; }

            [Display(Name = "Image")]
            public bool? ImageUrl { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public IFormFile file { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                if(User.Identity.IsAuthenticated){
                    await _signInManager.SignOutAsync();
                    _logger.LogInformation("User logged out.");
                }
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var UserManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var RoleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                    var roleResult = await RoleManager.FindByNameAsync("Supplier");
                    if (roleResult == null)
                    {
                        roleResult = new ApplicationRole("Supplier");
                        await RoleManager.CreateAsync(roleResult);
                    }
                    await UserManager.AddToRoleAsync(user, "Supplier");

                    FuelSupplier fuelSupplier = new FuelSupplier();
                    fuelSupplier.UserId = user.Id;
                    fuelSupplier.Name = Input.Name;
                    fuelSupplier.CountryId = Input.CountryId;
                    fuelSupplier.IsMiddler = Input.IsMiddler;

                    if (Input.file != null)
                    {
                        FileInfo fi = new FileInfo(Input.file.FileName);
                        var newFilename = "P" + fuelSupplier.Id + "_" + string.Format("{0:d}",
                                          (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var webPath = _hostingEnvironment.WebRootPath;
                        var path = Path.Combine("", webPath + @"\uploads\suppliers\" + newFilename);

                        var pathToSave = @"/uploads/suppliers/" + newFilename;

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Input.file.CopyToAsync(stream);
                        }
                        fuelSupplier.ImageUrl = pathToSave;
                    }

                    SupplierContact supplierContact1 = new SupplierContact();
                    supplierContact1.ContactId = 3;
                    supplierContact1.Value = Input.CompanyWebSite;

                    SupplierContact supplierContact2 = new SupplierContact();
                    supplierContact2.ContactId = 18;
                    supplierContact2.Value = _context.Country.Find(Input.CountryId) != null ?
                        _context.Country.Find(Input.CountryId).Name : "";

                    fuelSupplier.SupplierContact = new List<SupplierContact>();
                    fuelSupplier.SupplierContact.Add(supplierContact1);
                    fuelSupplier.SupplierContact.Add(supplierContact2);

                    SupplierContactPerson supplierContactPerson = new SupplierContactPerson();
                    supplierContactPerson.JobTitle = Input.Position;
                    supplierContactPerson.Name = Input.Name;
                    SupplierContactPersonContact supplierContactPersonContact = new SupplierContactPersonContact();
                    supplierContactPersonContact.ContactId = 7;
                    supplierContactPersonContact.Value = Input.Email;
                    supplierContactPerson.SupplierContactPersonContact = new List<SupplierContactPersonContact>();
                    supplierContactPerson.SupplierContactPersonContact.Add(supplierContactPersonContact);

                    fuelSupplier.SupplierContactPerson = new List<SupplierContactPerson>();
                    fuelSupplier.SupplierContactPerson.Add(supplierContactPerson);
                    
                    _context.FuelSupplier.Add(fuelSupplier);
                    _context.SaveChanges();

                    var contentAppName = _context.ContentManagement.Where(cm => cm.Name == "app_name")
                                   .FirstOrDefault();
                    string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    EmailBodyDefaultParams emailBodyDefaultParams = _context.EmailBodyDefaultParams
                        .Where(e => e.EmailTypeName == "confirm_mail").FirstOrDefault();
                    string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                    body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));
                    var simpleResponse = EmailSender.SendEmail(Input.Email, AppName, body);
                    TempData.Set("Toast", simpleResponse);
                    return LocalRedirect(ReturnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
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
