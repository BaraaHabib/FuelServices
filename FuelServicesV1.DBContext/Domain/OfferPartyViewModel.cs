using DBContext.Models;
using System.ComponentModel.DataAnnotations;

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
        public int OfferId { get; set; }

        [Required]
        public string PriceUnit { get; set; }
    }
}