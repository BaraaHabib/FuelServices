using DBContext.Models;
using FuelServices.DBContext.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.DBContext.Domain
{
    public class OfferViewModel : BaseDomain
    {
        public OfferViewModel()
        {
            SelectedAirports = new List<Airport>();
            FuelTypes = new List<int>();

            Airports = new List<int>();
            AirportOffers = new List<AirportOfferViewModel>();
        }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "From")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "To")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date should be greater than Start Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Supplier Name")]
        public FuelSupplier FuelSupplier { get; set; }

        public string Status { get; set; }

        public List<int> Airports { get; set; }

        public List<int> FuelTypes { get; set; }

        public List<Airport> SelectedAirports { get; set; }

        public List<AirportOfferViewModel> AirportOffers { get; set; }

        public bool IsActive { get; set; }

        public string DuesTaxesLevies { get; set; }
    }
}