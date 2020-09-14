using DBContext.Models;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.DBContext.Domain
{
    public class ContactRecordModel
    {
        public int Id { get; set; }

        public int FuelSupplierId { get; set; }

        [Display(Name = "Order")]
        public int ItemOrder { get; set; }

        [Required]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [Required]
        public string Value { get; set; }

        public string Description { get; set; }
    }
}