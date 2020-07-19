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
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [DataTable(DisplayName = "Valid", Order = 3)]
        public bool? IsValid { get; set; }

        public virtual ICollection<Advertisement> Advertisement { get; set; }
    }
}
