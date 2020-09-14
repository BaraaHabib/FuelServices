using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class PropertyViewModel : BaseDomain
    {
        public PropertyViewModel()
        {
            AirportProperty = new HashSet<AirportPropertyViewModel>();
        }

        public string Name { get; set; }

        public int? ContentType { get; set; }

        public int? SubCategoryId { get; set; }

        public bool? United { get; set; }

        public int? PaymentPackageId { get; set; }

        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
        public virtual ICollection<AirportPropertyViewModel> AirportProperty { get; set; }
    }
}