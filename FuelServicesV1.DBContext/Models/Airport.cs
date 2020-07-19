using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Airport : BaseEntity
    {
        public Airport()
        {
            AirportProperty = new HashSet<AirportProperty>();
            Request = new HashSet<Request>();
            AirportOffer = new HashSet<AirportOffer>();
            OfferPartyExcludes = new HashSet<OfferPartyExcludes>();
        }

        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [DataTable(IsVisible = false, Order = 3)]
        public int? CityId { get; set; }

        [DataTable(IsVisible = false, Order = 4)]
        public int? NearestCityId { get; set; }

        [DataTable(IsVisible = false, Order = 5)]
        public int? CountryId { get; set; }

        [DataTable(DisplayName = "ICAO", Order = 6)]
        public string Icao { get; set; }

        [DataTable(DisplayName = "IATA", Order = 7)]
        public string Iata { get; set; }

        [Display(Name = "Latitude")]
        public float? Lat { get; set; }

        [Display(Name = "Longitude")]
        public float? Long { get; set; }

        [DataTable(DisplayName = "Type", Order = 12)]
        public string Type { get; set; }
        public float? Variation { get; set; }

        public float? Elevation { get; set; }
        public int? Views { get; set; }

        public string Continent { get; set; }

        [Display(Name = "Country Code")]
        public string IsoCountry { get; set; }

        [Display(Name = "Region Code")]
        public string IsoRegion { get; set; }

        [Display(Name = "City/Region")]
        public string Municipality { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual City NearestCity { get; set; }
        public virtual ICollection<AirportProperty> AirportProperty { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<AirportOffer> AirportOffer { get; set; }
        public virtual ICollection<OfferPartyExcludes> OfferPartyExcludes { get; set; }
    }
}
