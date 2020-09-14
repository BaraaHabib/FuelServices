namespace FuelServices.DBContext.Domain
{
    public partial class AdvertisementPropertyViewModel : BaseDomain
    {
        public int? AdvertisementId { get; set; }
        public int? AdvertisementTypePropertyId { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }

        public virtual AdvertisementTypePropertyViewModel AdvertisementTypeProperty { get; set; }
    }
}