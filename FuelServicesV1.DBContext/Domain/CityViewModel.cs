
namespace FuelServices.DBContext.Domain
{
    public partial class CityViewModel : BaseDomain
    {
        public string Name { get; set; }

        public string NameASCII { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public string Capital { get; set; }

        public int? CountryId { get; set; }
    }
}
