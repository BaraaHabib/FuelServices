using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementCategory : BaseEntity
    {
        public AdvertisementCategory()
        {
            Advertisement = new HashSet<Advertisement>();
        }

        [Required]
        [Display(Name = "Name *")]
        public string Name { get; set; }

        public virtual ICollection<Advertisement> Advertisement { get; set; }
    }
}
