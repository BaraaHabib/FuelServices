using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class SupplierReview : BaseEntity
    {

        [Required]
        [DataTable(DisplayName = "Rate", Order = 2)]
        public int? Rate { get; set; }

        [DataTable(DisplayName = "Review", Order = 3)]
        public string Value { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 4)]
        public int? FuelSupplierId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 5)]
        public int? CustomerId { get; set; }

        [Required]
        [DataTable(DisplayName = "Date", Order = 6)]
        public DateTime ReviewDate { get; set; }

        public virtual FuelSupplier FuelSupplier { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
