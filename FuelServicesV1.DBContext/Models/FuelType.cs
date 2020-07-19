using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class FuelType : BaseEntity
    {
        public FuelType()
        {
            OfferFuelType = new HashSet<OfferFuelType>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        public virtual ICollection<OfferFuelType> OfferFuelType { get; set; }
    }
}
