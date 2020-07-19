namespace FuelServices.DBContext.Domain
{
    public partial class AdvertisementTypePropertyViewModel : BaseDomain
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public double? NumberOfPriceUnits { get; set; }

        public string Unit { get; set; }

    }
}
