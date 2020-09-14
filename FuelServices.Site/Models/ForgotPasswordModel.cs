namespace FuelServices.Site.Models
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}