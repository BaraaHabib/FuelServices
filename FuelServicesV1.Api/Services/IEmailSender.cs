using DBContext.Models;
using FuelServices.Api.Helpers;
using System.Threading.Tasks;

namespace FuelServices.Api.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        public string CreateEmailBody(EmailBodyDefaultParams emailBodyDefaultParams);

        SimpleResponse SendEmailNotification(string receiver, string subject, string message);

        SimpleResponse SendEmail(string receiver, string subject, string message);

        SimpleResponse SendEmail(ContactUs contactUs);
    }
}