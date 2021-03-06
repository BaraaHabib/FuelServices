﻿namespace FuelServices.DBContext.Domain
{
    public partial class AirportContactViewModel : BaseDomain
    {
        public int? ContactId { get; set; }

        public int? AirportId { get; set; }

        public string Value { get; set; }

        public int? PaymentPackageId { get; set; }

        public virtual ContactViewModel Contact { get; set; }
        public virtual PaymentPackageViewModel PaymentPackage { get; set; }
    }
}
