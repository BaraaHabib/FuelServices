using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class SupplierContactPerson : BaseEntity
    {
        public SupplierContactPerson()
        {
            SupplierContactPersonContact = new HashSet<SupplierContactPersonContact>();
        }

        
        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? FuelSupplierId { get; set; }

        [DataTable(DisplayName = "Name", Order = 3)]
        public string Name { get; set; }

        [DataTable(DisplayName = "Job Title", Order = 4)]
        public string JobTitle { get; set; }

        public virtual FuelSupplier FuelSupplier { get; set; }
        public virtual ICollection<SupplierContactPersonContact> SupplierContactPersonContact { get; set; }
    }
}
