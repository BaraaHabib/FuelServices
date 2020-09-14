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

        //[Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Name *")]
        public string DisplayName { get; set; }

        public string Options { get; set; }

        public string Unit { get; set; }

        public int? AdvertisementTypeId { get; set; }

        public int? ExceptAdvertisementType1Id { get; set; }

        public int? ExceptAdvertisementType2Id { get; set; }

        public virtual AdvertisementType AdvertisementType { get; set; }

        public virtual ICollection<AdvertisementProperty> AdvertisementProperty { get; set; }
    }
}
