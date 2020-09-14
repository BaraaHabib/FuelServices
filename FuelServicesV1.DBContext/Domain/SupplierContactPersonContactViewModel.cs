namespace FuelServices.DBContext.Domain
{
    public partial class SupplierContactPersonContactViewModel : BaseDomain
    {
        public int? ContactId { get; set; }

        public int? SupplierContactPersonId { get; set; }

        public string Value { get; set; }

        public int? PaymentPackageId { get; set; }

        public virtual ContactViewModel Contact { get; set; }
        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
        public virtual SupplierContactPersonViewModel SupplierContactPerson { get; set; }
    }
}