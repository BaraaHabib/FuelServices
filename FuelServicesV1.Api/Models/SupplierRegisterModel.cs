using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.Api.Models
{
    public class SupplierRegisterModel
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
}