using Elect.Web.DataTable.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class CustomerPackagesLog : BaseEntity
    {

        [DataTable(IsVisible = false, Order = 2)]
        public int? CustomerId { get; set; }

        [DataTable(IsVisible = false, Order = 3)]
        public int? PaymentPackageId { get; set; }

        [Required]
        [DataTable(DisplayName = "Started at date", Order = 4)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataTable(DisplayName = "Ends at date", Order = 5)]
        public DateTime EndDate { get; set; }

        [DataTable(DisplayName = "Name", Order = 2)]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Country", Order = 4)]
        [DataTable(IsVisible = false, Order = 6)]
        public int? CountryId { get; set; }

        [Required]
        [Display(Name = "City", Order = 4)]
        [DataTable(IsVisible = false, Order = 7)]
        public int? CityId { get; set; }

        [Required]
        [DataTable(DisplayName = "Postal Code", Order = 8)]
        public string PostalCode { get; set; }

        [Required]
        [DataTable(DisplayName = "First Address", Order = 9)]
        [Display(Name = "First Address", Order = 4)]
        public string Address1 { get; set; }

        [DataTable(DisplayName = "Second Address", Order = 10)]
        [Display(Name = "Second Address", Order = 4)]
        public string Address2 { get; set; }

        [Required]
        [DataTable(DisplayName = "First Phone", Order = 11)]
        [Display(Name = "First Phone", Order = 4)]
        public string Phone1 { get; set; }

        [Display(Name = "Second Phone", Order = 4)]
        [DataTable(DisplayName = "Second Phone", Order = 12)]
        public string Phone2 { get; set; }

        [DataTable(DisplayName = "Fax", Order = 13)]
        public string Fax { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual PaymentPackage PaymentPackage { get; set; }
    }
}
