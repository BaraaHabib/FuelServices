namespace FuelServices.DBContext.Domain
{
    public partial class PaymentPackageFeatureViewModel : BaseDomain
    {
        public int? PaymentPackageId { get; set; }

        public int? FeatureId { get; set; }

        public string Value { get; set; }

        public virtual FeatureViewModel Feature { get; set; }
        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
    }
}