using Elect.Web.DataTable.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AirportProperty : BaseEntity
    {

        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? AirportId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? PropertyId { get; set; }

        [Required]
        [DataTable(DisplayName = "Value", Order = 4)]
        public string Value { get; set; }

        [DataTable(DisplayName = "Unit", Order = 5)]
        public string Unit { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual Property Property { get; set; }
    }
}
