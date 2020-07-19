using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class RequestOffers : BaseEntity
    {
        
        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? RequestId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? OfferId { get; set; }

        [Required]
        public int? AirportOfferId { get; set; }


        [Required]
        [DataTable(DisplayName = "Reply Status", Order = 5)]
        public ReplyStatus RStatus { get; set; }

        public DateTime?  SupplierConfirmDate { get; set; }

        public virtual Request Request { get; set; }
        public virtual AirportOffer AirportOffer { get; set; }
        public virtual Offer Offer { get; set; }


    }

    public enum ReplyStatus : int
    {
        Success = 1,
        Pending = 0,
        Rejected = -1,
        Expired = 2,
        ApprovedBySupplier = 3,
        ConfirmedByCustomer = 4,
        AgreedWithASupplier = 5,
        WaitingForPayment = 6,

    }
}
