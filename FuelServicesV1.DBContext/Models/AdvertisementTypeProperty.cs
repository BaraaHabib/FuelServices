using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementTypeProperty : BaseEntity
    {
        public AdvertisementTypeProperty()
        {
            AdvertisementProperty = new HashSet<AdvertisementProperty>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        public virtual ICollection<AdvertisementProperty> AdvertisementProperty { get; set; }
    }
}
