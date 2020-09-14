using Elect.Web.DataTable.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementProperty : BaseEntity
    {

        [Display(Name = "Advertisement *")]
        public int? AdvertisementId { get; set; }

        [Display(Name = "Property *")]
        //[Required]
        public int? AdvertisementTypePropertyId { get; set; }

        [Required]
        //[Display(Name = "Value *")]
        public string Value { get; set; }

        public string Unit { get; set; }

        public virtual Advertisement Advertisement { get; set; }
        public virtual AdvertisementTypeProperty AdvertisementTypeProperty { get; set; }
    }
}
