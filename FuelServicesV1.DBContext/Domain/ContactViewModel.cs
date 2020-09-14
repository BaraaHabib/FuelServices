using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class ContactViewModel : BaseDomain
    {
        public ContactViewModel()
        {
            AirportContact = new HashSet<AirportContactViewModel>();
            AirportContactPersonContact = new HashSet<AirportContactPersonContactViewModel>();
            SupplierContact = new HashSet<SupplierContactViewModel>();
            SupplierContactPersonContact = new HashSet<SupplierContactPersonContactViewModel>();
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public virtual ICollection<AirportContactViewModel> AirportContact { get; set; }
        public virtual ICollection<AirportContactPersonContactViewModel> AirportContactPersonContact { get; set; }
        public virtual ICollection<SupplierContactViewModel> SupplierContact { get; set; }
        public virtual ICollection<SupplierContactPersonContactViewModel> SupplierContactPersonContact { get; set; }
    }
}