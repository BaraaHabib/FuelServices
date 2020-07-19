using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Offer : BaseEntity
    {
        public Offer()
        {
           // Request = new HashSet<Request>();
            OfferFuelType = new HashSet<OfferFuelType>();
            AirportOffers = new HashSet<AirportOffer>();
            OfferProperties = new HashSet<OfferProperties>();
            RequestOffers = new HashSet<RequestOffers>();
        }


        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataTable(DisplayName = "Start Date", Order = 2)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataTable(DisplayName = "End Date", Order = 3)]
        public DateTime EndDate { get; set; }

        

        [Required]
        [Display(Name = "Supplier")]
        [DataTable(IsVisible = false, Order = 6)]
        public int? FuelSupplierId { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataTable(DisplayName = "Status", Order = 7)]
        public string Status { get; set; }

        [Required]
        [Display(Name = "DUES, TAXES, LEVIES")]
        [DataTable(DisplayName = "DUES, TAXES, LEVIES", Order = 7)]
        public string DuesTaxesLevies { get; set; }

        public virtual FuelSupplier FuelSupplier { get; set; }
        public virtual ICollection<AirportOffer> AirportOffers { get; set; }
        //public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<OfferFuelType> OfferFuelType { get; set; }
        public virtual ICollection<OfferProperties> OfferProperties { get; set; }
        public virtual ICollection<RequestOffers> RequestOffers { get; set; }
    }

    public enum OfferStatus : int
    {
        Expired = 0,
        Active = 1,
        DeActivated = 2
    }
}