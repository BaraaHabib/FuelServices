using Elect.Web.DataTable.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class SupplierContactPersonContact : BaseEntity
    {

        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? ContactId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? SupplierContactPersonId { get; set; }

        [Required]
        [DataTable(DisplayName = "Value", Order = 4)]
        public string Value { get; set; }

        public string Description { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual SupplierContactPerson SupplierContactPerson { get; set; }
    }
}
