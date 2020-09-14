using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class PaymentPackageViewModel : BaseDomain
    {
        public PaymentPackageViewModel()
        {
            AirportContact = new HashSet<AirportContactViewModel>();
            AirportContactPersonContact = new HashSet<AirportContactPersonContactViewModel>();
            CustomerPackagesLog = new HashSet<CustomerPackagesLogViewModel>();
            PaymentPackageFeature = new HashSet<PaymentPackageFeatureViewModel>();
            Property = new HashSet<PropertyViewModel>();
            SupplierContact = new HashSet<SupplierContactViewModel>();
            SupplierContactPersonContact = new HashSet<SupplierContactPersonContactViewModel>();
        }

        public int? Type { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int? Period { get; set; }

        public string ImageUrl { get; set; }

        public double? Price { get; set; }

        public double? Discount { get; set; }

        public int? DiscountType { get; set; }

        public string DiscountUnit { get; set; }

        public int? ItemLevel { get; set; }

        public int? ColorPaletteId { get; set; }

        public virtual ColorPaletteViewModel ColorPalette { get; set; }
        public virtual ICollection<AirportContactViewModel> AirportContact { get; set; }
        public virtual ICollection<AirportContactPersonContactViewModel> AirportContactPersonContact { get; set; }
        public virtual ICollection<CustomerPackagesLogViewModel> CustomerPackagesLog { get; set; }
        public virtual ICollection<PaymentPackageFeatureViewModel> PaymentPackageFeature { get; set; }
        public virtual ICollection<PropertyViewModel> Property { get; set; }
        public virtual ICollection<SupplierContactViewModel> SupplierContact { get; set; }
        public virtual ICollection<SupplierContactPersonContactViewModel> SupplierContactPersonContact { get; set; }
    }
}