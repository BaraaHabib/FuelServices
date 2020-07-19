using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class SupplierType : BaseEntity
    {
        public SupplierType()
        {
            AirportOffer = new HashSet<AirportOffer>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        public virtual ICollection<AirportOffer> AirportOffer { get; set; }
    }
}
