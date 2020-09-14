namespace FuelServices.Api.Models
{
    public class AirportModel
    {
        public int Id { get; set; }

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

        public string PriceForOffer { get; set; }
        public string PriceUnitForOffer { get; set; }
    }
}