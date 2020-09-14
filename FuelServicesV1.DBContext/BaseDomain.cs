using System;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.DBContext
{
    public class BaseDomain
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Order")]
        public int ItemOrder { get; set; }
    }
}