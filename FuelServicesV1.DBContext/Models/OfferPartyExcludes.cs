using Elect.Web.DataTable.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class OfferPartyExcludes : BaseEntity
    {

        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? AirportOfferId { get; set; }

        [DataTable(IsVisible = false, Order = 3)]
        public int? AirportId { get; set; }

        [DataTable(IsVisible = false, Order = 4)]
        public int? CityId { get; set; }

        [DataTable(IsVisible = false, Order = 5)]
        public int? CountryId { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual AirportOffer AirportOffer { get; set; }
    }
}
