using System;
using System.Collections.Generic;

namespace FuelServices.Api.Models
{
    public class OfferModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string FuelSupplier { get; set; }

        public string Status { get; set; }

        public List<AirportModel> Airports { get; set; }

        public List<FuelTypeModel> FuelTypes { get; set; }

        public bool IsActive { get; set; }

        public string DuesTaxesLevies { get; set; }
        public string ImageUrl { get; set; }
    }
}