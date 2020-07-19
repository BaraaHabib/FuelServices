using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class SupplierViewModel : BaseDomain
    {
        public SupplierViewModel()
        {
            SupplierContact = new HashSet<SupplierContactViewModel>();
            SupplierContactPerson = new HashSet<SupplierContactPersonViewModel>();
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public int? CountryId { get; set; }

        public string ImageUrl { get; set; }

        public int? UserSpecializationId { get; set; }
        public int? CompanyId { get; set; }

        public virtual UserSpecializationViewModel UserSpecialization { get; set; }
        public virtual CountryViewModel Country { get; set; }
        public virtual ICollection<SupplierContactViewModel> SupplierContact { get; set; }
        public virtual ICollection<SupplierContactPersonViewModel> SupplierContactPerson { get; set; }
    }
}
