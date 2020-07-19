using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
            AdvertisementOwner = new HashSet<AdvertisementOwner>();
            Airport = new HashSet<Airport>();
            City = new HashSet<City>();
            Customer = new HashSet<Customer>();
            FuelSupplier = new HashSet<FuelSupplier>();
            OfferPartyExcludes = new HashSet<OfferPartyExcludes>();
        }

        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [DataTable(IsVisible = false, Order = 3)]
        public int? ContinentId { get; set; }

        public virtual Continent Continent { get; set; }
        public virtual ICollection<AdvertisementOwner> AdvertisementOwner { get; set; }
        public virtual ICollection<Airport> Airport { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<FuelSupplier> FuelSupplier { get; set; }
        public virtual ICollection<OfferPartyExcludes> OfferPartyExcludes { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
    }
}
