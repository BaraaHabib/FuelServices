using System.ComponentModel.DataAnnotations;

namespace FuelServices.DBContext.Domain
{
    public class AdvertisementOwnerViewModel : BaseDomain
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "First name *")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name *")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone *")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email *")]
        [EmailAddress]
        public string Email { get; set; }

        public string Fax { get; set; }

        [Required]
        [Display(Name = "Country *")]
        public int? CountryId { get; set; }

        public string ImageUrl { get; set; }
        public int? UserSpecializationId { get; set; }

        [Required]
        [Display(Name = "Company *")]
        public int? CompanyId { get; set; }

        public UserSpecializationViewModel UserSpecialization { get; set; }
        public CountryViewModel Country { get; set; }
    }
}