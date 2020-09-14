namespace FuelServices.DBContext.Domain
{
    public partial class AirportAdsViewModel : BaseDomain
    {
        public int? AdvertisementId { get; set; }

        public int? AirportId { get; set; }

        public int? Range { get; set; }

        public int? CaptionClicks { get; set; }

        public virtual AdvertisementViewModel Advertisement { get; set; }
        public virtual AirportViewModel Airport { get; set; }
    }
}