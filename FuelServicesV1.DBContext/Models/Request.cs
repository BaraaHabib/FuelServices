using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Request : BaseEntity
    {
        public Request()
        {
            RequestOffers = new HashSet<RequestOffers>();
        }

 
        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? CustomerId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? AirportId { get; set; }

        [DataTable(DisplayName = "Name Of Operator", Order = 4)]
        [Display(Name = "Name Of Operator")]
        public string NameOfOperator { get; set; }
        
        [DataTable(DisplayName = "Captain/Fm's Name", Order = 5)]
        [Display(Name = "Captain/Fm's Name")]
        public string CaptainFmName { get; set; }

        //[Required]
        [DataTable(DisplayName = "Request Date", Order = 3)]
        [Display(Name = "Request Date")]
        public DateTime SendDate { get; set; }

        [Required]
        [DataTable(DisplayName = "Arrival Date", Order = 4)]
        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [DataTable(DisplayName = "Departure Date", Order = 4)]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [DataTable(DisplayName = "Aircraft Tail NO.", Order = 5)]
        [Display(Name = "Aircraft Tail NO.")]
        public string RegistrationNumber { get; set; }

        [Required]
        [DataTable(DisplayName = "Aircraft Type", Order = 6)]
        [Display(Name = "Aircraft Type")]
        public string AircraftType { get; set; }

        [Required]
        [DataTable(DisplayName = "Call Sign", Order = 7)]
        [Display(Name = "Call Sign")]
        public string CallSign { get; set; }


        //public int? OfferId { get; set; }


        //[Required]
        //[DataTable(DisplayName = "Fuel Type", Order = 8)]
        //[Display(Name = "Fuel Type")]
        //public int? OfferFuelTypeId { get; set; }

        [Required]
        [DataTable(DisplayName = "Quantity", Order = 9)]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [DataTable(DisplayName = "Notes", Order = 10)]
        public string Notes { get; set; }

        public virtual Customer Customer { get; set; }
        
        //
        //public virtual OfferFuelType OfferFuelType  { get; set; }
        //public virtual Offer Offer { get; set; }
        //
        
        public virtual Airport Airport { get; set; }
        public virtual ICollection<RequestOffers> RequestOffers { get; set; }
    }
}
