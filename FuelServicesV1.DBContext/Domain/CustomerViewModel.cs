using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class CustomerViewModel : BaseDomain
    {
        public CustomerViewModel()
        {
            CustomerPackagesLog = new HashSet<CustomerPackagesLogViewModel>();
            ContactUs = new HashSet<ContactUsViewModel>();
        }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public int? CountryId { get; set; }

        public int? UserSpecializationId { get; set; }

        public int? CompanyId { get; set; }

        public virtual UserSpecializationViewModel UserSpecialization { get; set; }
        public virtual CountryViewModel Country { get; set; }
        public virtual ICollection<CustomerPackagesLogViewModel> CustomerPackagesLog { get; set; }
        public virtual ICollection<ContactUsViewModel> ContactUs { get; set; }
    }
}