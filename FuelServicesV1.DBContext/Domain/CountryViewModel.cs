namespace FuelServices.DBContext.Domain
{
    public class CountryViewModel : BaseDomain
    {
        public string Name { get; set; }

        public string ISO2 { get; set; }

        public string ISO3 { get; set; }

        public int? ContinentId { get; set; }
    }
}
