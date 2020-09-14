namespace FuelServices.DBContext.Domain
{
    public partial class AirportViewModel : BaseDomain
    {
        public string Name { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Icao { get; set; }

        public string Iata { get; set; }

        public float? Lat { get; set; }

        public float? Long { get; set; }

        public float? Variation { get; set; }

        public float? Elevation { get; set; }

        public string Type { get; set; }

        public int? Views { get; set; }

        public string Continent { get; set; }

        public string IsoCountry { get; set; }

        public string IsoRegion { get; set; }

        public string Municipality { get; set; }
    }
}