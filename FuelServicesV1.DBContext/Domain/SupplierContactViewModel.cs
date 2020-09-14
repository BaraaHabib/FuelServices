namespace FuelServices.DBContext.Domain
{
    public partial class SupplierContactViewModel : BaseDomain
    {
        public int? SupplierId { get; set; }

        public int? ContactId { get; set; }

        public string Value { get; set; }

        public int? PaymentPackageId { get; set; }

        public virtual ContactViewModel Contact { get; set; }
        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
        public virtual SupplierViewModel Supplier { get; set; }
    }
}