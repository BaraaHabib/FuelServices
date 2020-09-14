using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class AdvertisementImageTypeViewModel : BaseDomain
    {
        public string Name { get; set; }
        public double? NumberOfPriceUnits { get; set; }

        public virtual ICollection<AdvertisementViewModel> Advertisement { get; set; }
    }
}