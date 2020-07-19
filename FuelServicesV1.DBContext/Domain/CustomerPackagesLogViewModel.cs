
using System;

namespace FuelServices.DBContext.Domain
{
    public partial class CustomerPackagesLogViewModel : BaseDomain
    {

        public int? CustomerId { get; set; }

        public int? PaymentPackageId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Company { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }

        public string PostalCode { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Fax { get; set; }

        public virtual CityViewModel City { get; set; }
        public virtual CountryViewModel Country { get; set; }
        public virtual CustomerViewModel Customer { get; set; }
        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
    }
}
