using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog.Debugging;
using Microsoft.AspNetCore.Authorization;
using FuelServices.Api.Services;
using System.Text.Encodings.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using FuelServices.DBContext.Models;
using Microsoft.Extensions.Logging;

namespace FuelServices.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<LoginModel> _logger;

        public IdentityController(IServiceProvider provider, IHostingEnvironment env, IEmailSender emailSender, ILogger<LoginModel> logger) : base(provider)
        {
            _hostingEnvironment = env;
            _emailSender = emailSender;
            _logger = logger;
        }


        //[ClaimRequirement(ClaimTypes.Role, "Customer")]
        [Authorize(Roles = "Supplier")]
        //[Authorize]
        public async Task<JsonResult> Try()
        {
            var user = await GetCurrentUserAsync();
            return new JsonResult(User);
        }
        // POST: api/Identity
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginModel Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await SignInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        var user =await UserManager.FindByNameAsync(Model.Email);
                        
                        var handler = new JwtSecurityTokenHandler();
                        // authentication successful so generate jwt token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(config.Value.Secret);


                        var claims = new ClaimsIdentity();
                        // add user id
                        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        // add roles to token
                        var roles = await UserManager.GetRolesAsync(user);
                        claims.AddClaims(roles.Select(x => new Claim(ClaimTypes.Role, x)));
                        

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = claims,
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var stoken = tokenHandler.WriteToken(securityToken);
                        Serilog.Log.Information("User logged in.", "User logged in.", Model.Email);
                        return new JsonResult(new Response<string>(Constants.SUCCESS_CODE, stoken));
                    }
                    if (result.RequiresTwoFactor)
                    {
                        Serilog.Log.Information("Login with two factors is needed", "LoginWith2fa", Model.Email);
                        return new JsonResult(new Response<string>(Constants.TWO_FACTORS_AUTH_CODE,  Constants.TWO_FACTORS_AUTH));
                    }
                    if (result.IsLockedOut)
                    {
                        Serilog.Log.Error("User account locked out.", "Lockout", Model.Email);
                        return new JsonResult(new Response<string>(Constants.LOCKOUT_CODE,  Constants.LOCKOUT));
                    }
                    if (result.IsNotAllowed)
                    {
                        Serilog.Log.Error("User account not verified", "NotAllowed", Model.Email);
                        return new JsonResult(new Response<string>(Constants.NOT_VERIFIED_CODE,  Constants.NOT_VERIFIED));
                    }
                    else
                    {
                        Serilog.Log.Error("Invalid login attempt.", "Invalid login attempt.", Model.Email);
                        return new JsonResult(new Response<string>(Constants.INVALID_LOGIN_CODE,  Constants.INVALID_LOGIN));
                    }
                }
                // If we got this far, something failed, redisplay form
                Serilog.Log.Error("something failed", " something failed", Model.Email);
                return new JsonResult(new Response<string>(Constants.SOMETHING_WRONG_CODE,  Constants.SOMETHING_WRONG));
            }
            catch (Exception e)
            {
                Serilog.Log.Error("something failed", " something failed", Model.Email);
                return new JsonResult(new Response<string>(Constants.SOMETHING_WRONG_CODE,  e.Message ?? e.InnerException.Message ?? Constants.SOMETHING_WRONG));
            }
        }


        [HttpPost]
        public async Task<JsonResult> Register([FromBody] CustomerResgisterModel model)
        {

          
            try
            {//"jiyim76325@sweatmail.com"
                //var ree = EmailSender.SendEmail(model.Email, "sub", "mess");
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        Serilog.Log.Information("User created a new account with password.");

                        Customer customer = new Customer();
                        customer.UserId = user.Id;
                        db.Customer.Add(customer);
                        db.SaveChanges();
                        await UserManager.AddToRoleAsync(user, "Customer");

                        var contentAppName = db.ContentManagement.Where(cm => cm.Name == "app_name")
                                       .FirstOrDefault();
                        string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

                        var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                        var code = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);


                        // generate callback url 
                        string sc = this.Request.Scheme;
                        string host = this.Request.Host.ToString();
                        string controller = "Identity";
                        string action = "ConfirmEmail";


                        var callbackUrl = $"{sc}://{host}/{controller}/{action}?userId={user.Id}&code={code}";
                        //

                        EmailBodyDefaultParams emailBodyDefaultParams = db.EmailBodyDefaultParams
                            .Where(e => e.EmailTypeName == "confirm_mail").FirstOrDefault();
                        string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                        body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));
                        var simpleResponse = EmailSender.SendEmail(model.Email, AppName, body);
                        return new JsonResult(new Response<bool>(Constants.SUCCESS_CODE, true, "Please Confirm Your Email."));
                    }
                    else
                    {
                        string message = result.Errors.Select(x => x.Description).ToList().Aggregate((x, y) => 
                        {
                            return x +","+ y;
                        });
                        return new JsonResult(new Response<bool>(Constants.SOMETHING_WRONG_CODE, false,message ));

                    }
                }
                else
                {
                    Serilog.Log.Error("Register Customer", model.Email);
                    return new JsonResult(new Response<bool>(Constants.INVALID_INPUT_CODE,false, Constants.INVALID_INPUT));

                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Constants.LogTemplates.LOGIN_ERROR_EX, model.Email);
                return new JsonResult(new Response<bool>(Constants.SOMETHING_WRONG_CODE,false,GetExceptionMessage(e)));
            }
        }

        
        [HttpPost]
        public async Task<JsonResult> SupplierRegister([FromBody] SupplierRegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        await SignInManager.SignOutAsync();
                        Serilog.Log.Information("User logged out.");
                    }
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        Serilog.Log.Information("User created a new account with password.");


                        var roleResult = await RoleManager.FindByNameAsync("Supplier");
                        if (roleResult == null)
                        {
                            roleResult = new ApplicationRole() { Name = "Supplier" };
                            await RoleManager.CreateAsync(roleResult);
                        }
                        await UserManager.AddToRoleAsync(user, "Supplier");

                        FuelSupplier fuelSupplier = new FuelSupplier();
                        fuelSupplier.UserId = user.Id;
                        fuelSupplier.Name = model.Name;
                        fuelSupplier.CountryId = model.CountryId;
                        fuelSupplier.IsMiddler = model.IsMiddler;

                        if (model.file != null)
                        {
                            FileInfo fi = new FileInfo(model.file.FileName);
                            var newFilename = "P" + fuelSupplier.Id + "_" + string.Format("{0:d}",
                                              (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                            var webPath = _hostingEnvironment.WebRootPath;
                            var path = Path.Combine("", webPath + @"\uploads\suppliers\" + newFilename);

                            var pathToSave = @"/uploads/suppliers/" + newFilename;

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await model.file.CopyToAsync(stream);
                            }
                            fuelSupplier.ImageUrl = pathToSave;
                        }

                        SupplierContact supplierContact1 = new SupplierContact();
                        supplierContact1.ContactId = 3;
                        supplierContact1.Value = model.CompanyWebSite;

                        SupplierContact supplierContact2 = new SupplierContact();
                        supplierContact2.ContactId = 18;
                        supplierContact2.Value = db.Country.Find(model.CountryId) != null ?
                            db.Country.Find(model.CountryId).Name : "";

                        fuelSupplier.SupplierContact = new List<SupplierContact>();
                        fuelSupplier.SupplierContact.Add(supplierContact1);
                        fuelSupplier.SupplierContact.Add(supplierContact2);

                        SupplierContactPerson supplierContactPerson = new SupplierContactPerson();
                        supplierContactPerson.JobTitle = model.Position;
                        supplierContactPerson.Name = model.Name;
                        SupplierContactPersonContact supplierContactPersonContact = new SupplierContactPersonContact();
                        supplierContactPersonContact.ContactId = 7;
                        supplierContactPersonContact.Value = model.Email;
                        supplierContactPerson.SupplierContactPersonContact = new List<SupplierContactPersonContact>();
                        supplierContactPerson.SupplierContactPersonContact.Add(supplierContactPersonContact);

                        fuelSupplier.SupplierContactPerson = new List<SupplierContactPerson>();
                        fuelSupplier.SupplierContactPerson.Add(supplierContactPerson);

                        db.FuelSupplier.Add(fuelSupplier);
                        db.SaveChanges();

                        var contentAppName = db.ContentManagement.Where(cm => cm.Name == "app_name")
                                       .FirstOrDefault();
                        string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

                        var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                        var code = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        EmailBodyDefaultParams emailBodyDefaultParams = db.EmailBodyDefaultParams
                            .Where(e => e.EmailTypeName == "confirm_mail").FirstOrDefault();
                        string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                        body = body.Replace("{callbackurl}", HtmlEncoder.Default.Encode(callbackUrl));
                        var simpleResponse = EmailSender.SendEmail(model.Email, AppName, body);
                        //var token = GetTokenForUser(user);
                        return new JsonResult(new Response<bool>(Constants.SUCCESS_CODE, true, simpleResponse.Message));

                    }
                    else
                    {
                        string errors = "";
                        foreach (var error in result.Errors)
                        {
                            errors += error;
                        }
                        Serilog.Log.Error("Register Supplier",model.Email, errors);
                    }
                }

                else
                {
                    return new JsonResult(new Response<bool>(Constants.INVALID_INPUT_CODE, false,Constants.INVALID_INPUT));

                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Constants.LogTemplates.LOGIN_ERROR_EX, model.Email);
                return new JsonResult(new Response<bool>(Constants.SOMETHING_WRONG_CODE,false,GetExceptionMessage(e)));
            }

            return new JsonResult(new Response<bool>(Constants.SOMETHING_WRONG_CODE,false, Constants.SOMETHING_WRONG));
        }


        [HttpPost]
        public async Task<JsonResult> Logout([FromBody] LoginModel Model)
        {
            Serilog.Log.Information(Constants.LogTemplates.LOGOUT, Model.Email);
            await SignInManager.SignOutAsync();
            return new JsonResult(new Response<string>(Constants.SUCCESS_CODE,
                Constants.SUCCESS));

        }

        public async Task<object> ConfirmEmail(string userId, string code)
        {
            string sc = this.Request.Scheme;
            string host = this.Request.Host.ToString().Replace("api.", "");
            string area = "Identity";
            string page = "Account/ConfirmEmail";
            string loginPage = "Account/Login";
            string redirecturl = $"{sc}://{host}/{area}/{loginPage}";
            //var callbackUrl = $"{sc}://{host}/{area}/{page}?userId={userId}&code={code}";

            try
            {

                if (userId == null || code == null)
                {
                    return Redirect(redirecturl);
                }

                var user = await UserManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }

                var codeDecodedBytes = WebEncoders.Base64UrlDecode(code);
                var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

                var result = await UserManager.ConfirmEmailAsync(user, codeDecoded);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
                }
                return Redirect(redirecturl);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Constants.LogTemplates.CONFIRM_ACCOUNT_EX, $"userid = '{userId}'");
                return Redirect(redirecturl);
            }
        }

        [HttpGet]
        public async Task<object> ForgotPassword(string Email)
        {
            try
            {
                if(Email == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);

                var user = await UserManager.FindByEmailAsync(Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                }
                var rand = new RandomGenerator();
                var code = rand.RandomToken();

                EmailBodyDefaultParams emailBodyDefaultParams = db.EmailBodyDefaultParams
                               .Where(e => e.EmailTypeName == "reset_password_api").FirstOrDefault();
                string body = EmailSender.CreateEmailBody(emailBodyDefaultParams);
                body = body.Replace("{callbackurl}", "#");
                body = body.Replace("{callbackdisplaytext}", code);

                var contentAppName = db.ContentManagement.Where(cm => cm.Name == "app_name")
                               .FirstOrDefault();
                string AppName = contentAppName == null ? "Fuel Services" : contentAppName.DisplayName;

                var simpleResponse = EmailSender.SendEmail(Email, AppName, body);

                ResetPasswordToken resetPasswordToken = new ResetPasswordToken()
                {
                    ResetPasswordCode = code,
                    UserId = user.Id,
                    ResetPasswordCodeValidityEndDate = DateTime.Now.AddMinutes(ResetPasswordConstants.Validity),
                };
                var prevAttempts = db.ResetPasswordTokens.Where(x => x.UserId == user.Id && x.Status == ResetPasswordTokenStatus.PendingValidation)
                    .ToList();
                prevAttempts.ForEach((x) => {
                    x.Status = ResetPasswordTokenStatus.Expired;
                });
                db.ResetPasswordTokens.UpdateRange(prevAttempts);
                db.ResetPasswordTokens.Add(resetPasswordToken);
                await db.SaveChangesAsync();

                return new Response<bool>(Constants.SUCCESS_CODE, true, Constants.SUCCESS);

            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, Constants.LogTemplates.RESET_PASSWORD_EX, Email);
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(e));
            }

        }

        [HttpPost]
        public async Task<object> ForgotPasswordConfirmation([FromBody]ForgotPasswordModel model)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                }

                var allAttempts = db.ResetPasswordTokens
                    .Where(x => x.UserId == user.Id)
                    .ToList();

                var resetpass = allAttempts.Where(x => x.ResetPasswordCode == model.Code).FirstOrDefault();


                if (resetpass != null)
                {
                    allAttempts.Remove(resetpass);
                    if (resetpass.Status == ResetPasswordTokenStatus.Expired)
                        return new Response<bool>(Constants.RESET_PASSWORD_ERR_CODE, false, "Code is Expired");

                    if (DateTime.Now > resetpass.ResetPasswordCodeValidityEndDate)
                    {
                        resetpass.Status = ResetPasswordTokenStatus.Expired;
                        db.ResetPasswordTokens.Update(resetpass);
                        return new Response<bool>(Constants.RESET_PASSWORD_ERR_CODE, false, "Code is Expired");
                    }
                    else if (resetpass.Status == ResetPasswordTokenStatus.Validated)
                    {
                        return new Response<bool>(Constants.RESET_PASSWORD_ERR_CODE, false, "You already used this code. Try again.");
                    }
                    else
                    {
                        // close previous attempts
                        allAttempts.Where(x => x.Status == ResetPasswordTokenStatus.PendingValidation)
                            .ToList().ForEach((x) =>
                            {
                                x.Status = ResetPasswordTokenStatus.Expired;
                            });
                        db.ResetPasswordTokens.UpdateRange(allAttempts);

                        // check old password
                        string hashedOldPassword = UserManager.PasswordHasher.HashPassword(user, model.OldPassword);
                        var res= UserManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);
                        if (res == PasswordVerificationResult.Failed)
                        {
                            return new Response<bool>(Constants.RESET_PASSWORD_ERR_CODE, false, "Invalid Password");
                        }

                        // set new password
                        String hashedNewPassword = UserManager.PasswordHasher.HashPassword(user, model.NewPassword);
                        user.PasswordHash = hashedNewPassword;
                        await UserManager.UpdateAsync(user);

                        // set to validated
                        resetpass.Status = ResetPasswordTokenStatus.Validated;
                        UserManager.PasswordHasher.HashPassword(user, model.OldPassword);
                            await db.SaveChangesAsync();
                        return new Response<bool>(Constants.SUCCESS_CODE, true, Constants.SUCCESS);

                    }
                }
                else
                {
                    return new Response<bool>(Constants.RESET_PASSWORD_ERR_CODE, false, "Invalid Code");
                }

            }
            catch (Exception e)
            {

                Serilog.Log.Error(e, Constants.LogTemplates.RESET_PASSWORD_EX, model.Email);
                return new Response<bool>(Constants.RESET_PASSWORD_ERR_CODE, false, GetExceptionMessage(e));

            }

        }

        private class RandomGenerator
        {
            // Generate a random number between two numbers    
            public int RandomNumber(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max);
            }

            // Generate a random string with a given size    
            public string RandomString(int size, bool lowerCase)
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                if (lowerCase)
                    return builder.ToString().ToLower();
                return builder.ToString();
            }

            // Generate a random password    
            public string RandomToken()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(RandomString(2, true));
                builder.Append(RandomNumber(100, 999));
                builder.Append(RandomString(2, false));
                return builder.ToString();
            }
        }

        private string GetTokenForUser(ApplicationUser user)
        {
            if (user == null)
                return null;
            var handler = new JwtSecurityTokenHandler();
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var stoken = tokenHandler.WriteToken(securityToken);
            return stoken;
        }
    }
}
