using DBContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FuelServices.DBContext.Domain
{
    public class AirportOfferViewModel
    {
        [Required]
        public int AirportId { get; set; }

        public virtual Airport Airport { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int OfferId{ get; set; }

        [Required]
        public string PriceUnit { get; set; }

    }
}
