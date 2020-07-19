using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class AdvertisementTypeViewModel : BaseDomain
    {
        public AdvertisementTypeViewModel()
        {
            Advertisement = new HashSet<AdvertisementViewModel>();
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public double? PriceOfUnit { get; set; }

        public string UnitOfPrice { get; set; }

        public virtual ICollection<AdvertisementViewModel> Advertisement { get; set; }
    }
}
