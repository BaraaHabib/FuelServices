
using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class FeatureViewModel : BaseDomain
    {
        public FeatureViewModel()
        {
            PaymentPackageFeature = new HashSet<PaymentPackageFeatureViewModel>();
        }

        public string Name { get; set; }

        public virtual ICollection<PaymentPackageFeatureViewModel> PaymentPackageFeature { get; set; }
    }
}
