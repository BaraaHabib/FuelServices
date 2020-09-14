using Elect.Web.DataTable.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class SupplierContact : BaseEntity
    {

        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? FuelSupplierId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? ContactId { get; set; }

        [Required]
        [DataTable(DisplayName = "Value", Order = 4)]
        public string Value { get; set; }

        public string Description { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual FuelSupplier FuelSupplier { get; set; }
    }
}
