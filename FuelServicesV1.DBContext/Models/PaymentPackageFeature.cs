using Elect.Web.DataTable.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class PaymentPackageFeature : BaseEntity
    {

        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? PaymentPackageId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? FeatureId { get; set; }

        [Required]
        [DataTable(DisplayName = "Value", Order = 4)]
        public string Value { get; set; }

        [DataTable(DisplayName = "Unit", Order = 5)]
        public string Unit { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual PaymentPackage PaymentPackage { get; set; }
    }
}
