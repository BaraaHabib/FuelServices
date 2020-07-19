using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Advertisement : BaseEntity
    {
        public Advertisement()
        {
            AdvertisementProperty = new HashSet<AdvertisementProperty>();
        }


        [DataTable(DisplayName = "Title", Order = 2)]
        public string Title { get; set; }

        [DataTable(DisplayName = "Description", Order = 3)]
        public string Description { get; set; }

        [DataTable(DisplayName = "Link", Order = 4)]
        public string AnchorUrl { get; set; }

        [DataTable(DisplayName = "Caption", Order = 5)]
        public string Caption { get; set; }

        [Required]
        [DataTable(DisplayName = "Image", Order = 6)]
        public string ImageUrl { get; set; }

        [Required]
        [DataTable(DisplayName = "Clicks Count", Order = 6)]
        public int? CaptionClicks { get; set; }

        [DataTable(IsVisible = false, Order = 7)]
        public int? AdvertisementOwnerId { get; set; }

        [DataTable(IsVisible = false, Order = 8)]
        public int? AdvertisementCategoryId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 9)]
        public int? AdvertisementTypeId { get; set; }

        [Required]
        [DataTable(DisplayName = "Price", Order = 10)]
        public float? Price { get; set; }


        [Required]
        [DataTable(DisplayName = "Publish Date", Order = 12)]
        public DateTime PublishDate { get; set; }

        [Required]
        [DataTable(DisplayName = "End Date", Order = 13)]
        public DateTime EndDate { get; set; }

        public virtual AdvertisementCategory AdvertisementCategory { get; set; }
        public virtual AdvertisementOwner AdvertisementOwner { get; set; }
        public virtual AdvertisementType AdvertisementType { get; set; }
        public virtual ICollection<AdvertisementProperty> AdvertisementProperty { get; set; }
    }
}
