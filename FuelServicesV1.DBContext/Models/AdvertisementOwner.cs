using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementOwner : BaseEntity
    {
        public AdvertisementOwner()
        {
            Advertisement = new HashSet<Advertisement>();
        }


        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public string UserId { get; set; }

        [Required]
        [DataTable(DisplayName = "First Name", Order = 3)]
        public string FirstName { get; set; }

        [Required]
        [DataTable(DisplayName = "Last Name", Order = 4)]
        public string LastName { get; set; }

        [Required]
        [DataTable(DisplayName = "Phone", Order = 5)]
        public string Phone { get; set; }

        [Required]
        [DataTable(DisplayName = "Email", Order = 6)]
        public string Email { get; set; }

        [DataTable(DisplayName = "Fax", Order = 7)]
        public string Fax { get; set; }

        [DataTable(IsVisible = false, Order = 8)]
        public int? CountryId { get; set; }

        [DataTable(DisplayName = "Image Url", Order = 9)]
        public string ImageUrl { get; set; }

        public virtual Country Country { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Advertisement> Advertisement { get; set; }
    }
}
