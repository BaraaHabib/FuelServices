using System;

namespace FuelServices.DBContext.Domain
{
    public partial class AdvertisementViewModel : BaseDomain
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AnchorUrl { get; set; }
        public string Caption { get; set; }
        public int? CaptionClicks { get; set; }
        public int? AdvertisementOwnerId { get; set; }
        public int? AdvertisementCategoryId { get; set; }
        public int? AdvertisementTypeId { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public int? AdvertisementImageTypeId { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? TotalPrice { get; set; }
    }
}