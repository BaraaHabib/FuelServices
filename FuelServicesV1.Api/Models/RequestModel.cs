﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Models
{
    public class RequestModel
    {
        //[Required]
        public int? CustomerId { get; set; }


        [Required]
        public int? AirportId { get; set; }


        [Required]
        public int? OfferId { get; set; }


        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string AircraftType { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string CallSign { get; set; }

        [Required]
        public string Notes { get; set; }


        // request details properties
        public string Customer { get; set; } = "";
        
        public string Airport { get; set; } = "";

        public string Supplier { get; set; } = "";

        public string Status { get; set; }

        public string Price { get; set; }

        public string PriceUnit { get; set; }

        public DateTime requestDate { get; set; }

        public int Id { get; set; }

    }
}
