using Elect.Web.DataTable.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBContext.Models
{
    public partial class AdvertisementOwner : BaseEntity
    {
        public AdvertisementOwner()
        {
            Advertisement = new HashSet<Advertisement>();
        }

        [Required]
        [Display(Name = "User *")]
        public string UserId { get; set; }

        [Display(Name = "First name *")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name *")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Phone *")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Email *")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Fax { get; set; }

        [Display(Name = "Country *")]
        [Required]
        public int? CountryId { get; set; }

        [Display(Name = "Image path")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "User Specialization *")]
        public int? UserSpecializationId { get; set; }

        [Display(Name = "Company *")]
        public int? CompanyId { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }

        public virtual Country Country { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Advertisement> Advertisement { get; set; }
    }
}
