using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Contact : BaseEntity
    {
        public Contact()
        {
            SupplierContact = new HashSet<SupplierContact>();
            SupplierContactPersonContact = new HashSet<SupplierContactPersonContact>();
        }

        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [Required]
        [DataTable(DisplayName = "Display Name", Order = 3)]
        public string DisplayName { get; set; }

        public virtual ICollection<SupplierContact> SupplierContact { get; set; }
        public virtual ICollection<SupplierContactPersonContact> SupplierContactPersonContact { get; set; }
    }
}
