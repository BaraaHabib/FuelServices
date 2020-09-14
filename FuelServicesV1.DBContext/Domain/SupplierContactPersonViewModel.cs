using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class SupplierContactPersonViewModel : BaseDomain
    {
        public SupplierContactPersonViewModel()
        {
            SupplierContactPersonContact = new HashSet<SupplierContactPersonContactViewModel>();
        }

        public int? SupplierId { get; set; }

        public string Name { get; set; }

        public string JobTitle { get; set; }

        public virtual SupplierViewModel Supplier { get; set; }
        public virtual ICollection<SupplierContactPersonContactViewModel> SupplierContactPersonContact { get; set; }
    }
}