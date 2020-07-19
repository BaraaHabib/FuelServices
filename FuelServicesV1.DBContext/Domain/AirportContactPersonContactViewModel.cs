namespace FuelServices.DBContext.Domain
{
    public partial class AirportContactPersonContactViewModel : BaseDomain
    {
        public int? ContactId { get; set; }

        public int? AirportContactPersonId { get; set; }

        public string Value { get; set; }

        public int? PaymentPackageId { get; set; }

        public virtual AirportContactPersonViewModel AirportContactPerson { get; set; }
        public virtual ContactViewModel Contact { get; set; }
        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
    }
}
