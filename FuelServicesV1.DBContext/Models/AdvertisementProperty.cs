using Elect.Web.DataTable.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementProperty : BaseEntity
    {

        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? AdvertisementId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? AdvertisementTypePropertyId { get; set; }

        [Required]
        [DataTable(DisplayName = "Value", Order = 4)]
        public string Value { get; set; }

        [DataTable(DisplayName = "Unit", Order = 5)]
        public string Unit { get; set; }

        public virtual Advertisement Advertisement { get; set; }
        public virtual AdvertisementTypeProperty AdvertisementTypeProperty { get; set; }
    }
}
