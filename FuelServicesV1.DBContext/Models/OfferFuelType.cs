using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class OfferFuelType : BaseEntity
    {
        public OfferFuelType()
        {
           // Request = new HashSet<Request>();
        }

        
        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? FuelTypeId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? OfferId { get; set; }

        public virtual FuelType FuelType { get; set; }
        public virtual Offer Offer { get; set; }
        //public virtual ICollection<Request> Request { get; set; }
    }
}
