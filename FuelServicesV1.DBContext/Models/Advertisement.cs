using Elect.Web.DataTable.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBContext.Models
{
    public partial class Advertisement : BaseEntity
    {
        public Advertisement()
        {
            AdvertisementProperty = new HashSet<AdvertisementProperty>();
            AirportAds = new HashSet<AirportAds>();
        }

        [Display(Name = "Title *")]
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Link *")]
        [Required]
        public string AnchorUrl { get; set; }

        [Display(Name = "Text to Display")]
        public string Caption { get; set; }

        [Display(Name = "Number of clicks *")]
        public int? CaptionClicks { get; set; }

        [Required]
        [Display(Name = "Owner *")]
        public int? AdvertisementOwnerId { get; set; }

        [Required]
        [Display(Name = "Category *")]
        public int? AdvertisementCategoryId { get; set; }

        [Required]
        [Display(Name = "Type *")]
        public int? AdvertisementTypeId { get; set; }

        [Required]
        [Display(Name = "Status *")]
        public string Status { get; set; }

        [Display(Name = "Image file *")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Image type *")]
        public int? AdvertisementImageTypeId { get; set; }

        [Required]
        [Display(Name = "Publish from date *")]
        public DateTime? PublishDate { get; set; }

        [Required]
        [Display(Name = "To date *")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Approximate price")]
        public double? TotalPrice { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }

        public virtual AdvertisementCategory AdvertisementCategory { get; set; }
        public virtual AdvertisementOwner AdvertisementOwner { get; set; }
        public virtual AdvertisementType AdvertisementType { get; set; }
        public virtual AdvertisementImageType AdvertisementImageType { get; set; }
        public virtual ICollection<AdvertisementProperty> AdvertisementProperty { get; set; }
        public virtual ICollection<AirportAds> AirportAds { get; set; }
    }
}
