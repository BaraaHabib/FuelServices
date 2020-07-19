using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class City : BaseEntity
    {
        public City()
        {
            AirportCity = new HashSet<Airport>();
            AirportNearestCity = new HashSet<Airport>();
            AirportOffer = new HashSet<AirportOffer>();
            OfferPartyExcludes = new HashSet<OfferPartyExcludes>();
        }

        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? CountryId { get; set; }

        [DataTable(DisplayName = "State", Order = 4)]
        public string State { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Airport> AirportCity { get; set; }
        public virtual ICollection<Airport> AirportNearestCity { get; set; }
        public virtual ICollection<AirportOffer> AirportOffer { get; set; }
        public virtual ICollection<OfferPartyExcludes> OfferPartyExcludes { get; set; }
        public string NameASCII { get; set; }
    }
}
