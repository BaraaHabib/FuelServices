using System.ComponentModel.DataAnnotations;

namespace FuelServices.Api.Models
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}