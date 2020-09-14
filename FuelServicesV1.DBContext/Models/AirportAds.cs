using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class AirportAds : BaseEntity
    {

        [Required]
        [Display(Name = "Advertisement *")]
        public int? AdvertisementId { get; set; }

        [Required]
        [Display(Name = "Airport *")]
        public int? AirportId { get; set; }

        [Required]
        [Display(Name = "Range *")]
        public int? Range { get; set; }

        [Display(Name = "Number of clicks on advertisement *")]
        public int? CaptionClicks { get; set; }

        public virtual Advertisement Advertisement { get; set; }
        public virtual Airport Airport { get; set; }
    }
}
