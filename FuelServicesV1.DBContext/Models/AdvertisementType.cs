using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementType : BaseEntity
    {
        public AdvertisementType()
        {
            Advertisement = new HashSet<Advertisement>();
            AdvertisementTypeProperty = new HashSet<AdvertisementTypeProperty>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Name *")]
        public string DisplayName { get; set; }

        public virtual ICollection<Advertisement> Advertisement { get; set; }
        public virtual ICollection<AdvertisementTypeProperty> AdvertisementTypeProperty { get; set; }
    }
}
