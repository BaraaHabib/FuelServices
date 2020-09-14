using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AdvertisementImageType : BaseEntity
    {
        [Required]
        [Display(Name = "Name *")]
        public string Name { get; set; }

        public virtual ICollection<Advertisement> Advertisement { get; set; }
    }
}
