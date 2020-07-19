using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Property : BaseEntity
    {
        public Property()
        {
            AirportProperty = new HashSet<AirportProperty>();
            OfferProperties = new HashSet<OfferProperties>();
            SupplierProperties = new HashSet<SupplierProperties>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [Required]
        [DataTable(DisplayName = "Content Type", Order = 3)]
        public int? ContentType { get; set; }

        [Required]
        [DataTable(DisplayName = "With Unit?", Order = 5)]
        public bool? United { get; set; }

        public virtual ICollection<AirportProperty> AirportProperty { get; set; }
        public virtual ICollection<OfferProperties> OfferProperties { get; set; }
        public virtual ICollection<SupplierProperties> SupplierProperties { get; set; }

    }
}
