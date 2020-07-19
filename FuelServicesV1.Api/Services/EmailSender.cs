using DBContext.Models;
using FuelServices.Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FuelServices.Api.Services
{
    public class EmailSender : IEmailSender
    {
        private static IHostingEnvironment _hostingEnvironment;

        public EmailSender(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        public static string CreateEmailBody(EmailBodyDefaultParams emailBodyDefaultParams)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   
            string rootPath = "";
            if(_hostingEnvironment.WebRootPath == null)
            {
               rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            else
            {
                rootPath = _hostingEnvironment.WebRootPath;
            }
            using (StreamReader reader = new StreamReader(Path.Combine(rootPath, emailBodyDefaultParams.TemplateUrl)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{sitelink}", emailBodyDefaultParams.SiteUrl);
            body = body.Replace("{title1}", emailBodyDefaultParams.Title1);
            body = body.Replace("{title2}", emailBodyDefaultParams.Title2);
            body = body.Replace("{banner}", emailBodyDefaultParams.Banner);
            body = body.Replace("{title3}", emailBodyDefaultParams.Title3);
            body = body.Replace("{contactemail}", emailBodyDefaultParams.ContactEmail);
            body = body.Replace("{cardcolor}", emailBodyDefaultParams.CardColor);
            body = body.Replace("{backgroundcolor}", emailBodyDefaultParams.BackgroundColor);
            body = body.Replace("{fontcolor}", emailBodyDefaultParams.FontColor);

            if (emailBodyDefaultParams.EmailTypeName == Constants.CONFIRMATION_EMAIL_TYPE
                || emailBodyDefaultParams.EmailTypeName == Constants.RESET_PASSWORD_EMAIL_TYPE)
            {
                body = body.Replace("{callbackdisplaytext}", emailBodyDefaultParams.CallbackDisplayText);
                body = body.Replace("{sitelogo}", emailBodyDefaultParams.Logo);
                body = body.Replace("{footer}", emailBodyDefaultParams.EmailCaption);
            }

            return body;

        }

        public static SimpleResponse SendEmail(string receiver, string subject, string message)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("v1vontactmail@gmail.com", "G6f0wf6~");

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("v1vontactmail@gmail.com"),
                    Subject = subject,
                    Body = message
                };

                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(receiver));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);

                Serilog.Log.Information("Email sent successfully", "Email sent successfully", receiver);
                return new SimpleResponse(Constants.SUCCESS_CODE, "Verification email sent." +
                    " Please check your email.");
            }
            catch (System.Exception e)
            {
                Serilog.Log.Error(e.Message, e.Message, receiver);
                return new SimpleResponse(Constants.SOMETHING_WRONG_CODE, e.Message);
            }
        }

        public static SimpleResponse SendEmail(ContactUs contactUs)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("v1vontactmail@gmail.com", "G6f0wf6~");
                string message = "";
                message += "Name:" + contactUs.FirstName + " " + contactUs.LastName + "\n";
                message += "Email:" + contactUs.Email + "\n";
                message += "Tel:" + contactUs.Tel + "\n";
                message += "Subject:" + contactUs.Subject + "\n";
                message += "Message:" + contactUs.Message + "\n";

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("v1vontactmail@gmail.com"),
                    Subject = contactUs.Subject,
                    Body = message
                };

                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress("v1vontactmail@gmail.com"));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);

                Serilog.Log.Information("Contact us sent successfully", "Contact us sent successfully", contactUs.Email);
                return new SimpleResponse(Constants.SUCCESS_CODE, "Sent Successfully");
            }
            catch (System.Exception e)
            {
                Serilog.Log.Error(e.Message, e.Message, contactUs.Email);
                return new SimpleResponse(Constants.SOMETHING_WRONG_CODE, e.Message);
            }
        }
    }
}
