using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBContext.Models
{
    [Table("AirportOffer")]
    public partial class AirportOffer : BaseEntity
    {
        public AirportOffer()
        {
            OfferPartyExcludes = new HashSet<OfferPartyExcludes>();
        }


        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? OfferId { get; set; }

        [DataTable(IsVisible = false, Order = 3)]
        public int? AirportId { get; set; }

        [DataTable(IsVisible = false, Order = 4)]
        public int? CityId { get; set; }



        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price Unit")]
        public string PriceUnit { get; set; }


        [DataTable(IsVisible = false, Order = 8)]
        public int? SupplierTypeId { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual City City { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual SupplierType SupplierType { get; set; }
        public virtual ICollection<OfferPartyExcludes> OfferPartyExcludes { get; set; }
        public virtual ICollection<RequestOffers> RequestOffers { get; set; }

    }

    public enum OfferRange : int
    {
        [Display(Name = "Only one airport")]
        Airport = 1,

        [Display(Name = "Whole city")]
        City = 2,

        [Display(Name = "Whole country")]
        Country = 3,

        [Display(Name = "Whole continent")]
        Continent = 4
    }
}
