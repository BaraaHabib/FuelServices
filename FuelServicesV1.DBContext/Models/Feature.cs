using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Feature : BaseEntity
    {
        public Feature()
        {
            PaymentPackageFeature = new HashSet<PaymentPackageFeature>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [DataTable(DisplayName = "Unit", Order = 3)]
        public string UnitIn { get; set; }


        public virtual ICollection<PaymentPackageFeature> PaymentPackageFeature { get; set; }
    }
}
