using Elect.Web.DataTable.Attributes;
using FuelServices.DBContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class FuelSupplier : BaseEntity
    {
        public FuelSupplier()
        {
            SupplierContact = new HashSet<SupplierContact>();
            SupplierContactPerson = new HashSet<SupplierContactPerson>();
            Offer = new HashSet<Offer>();
            SupplierReview = new HashSet<SupplierReview>();
            SupplierProperties = new HashSet<SupplierProperties>();
        }

        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [DataTable(IsVisible = false, Order = 3)]
        public string UserId { get; set; }

        [DataTable(IsVisible = false, Order = 4)]
        public int? CountryId { get; set; }

        [DataTable(DisplayName = "Image", Order = 5)]
        public string ImageUrl { get; set; }

        [DataTable(DisplayName = "Is Middler?", Order = 6)]
        public bool? IsMiddler { get; set; }

        public virtual Country Country { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<SupplierContact> SupplierContact { get; set; }
        public virtual ICollection<SupplierContactPerson> SupplierContactPerson { get; set; }
        public virtual ICollection<Offer> Offer { get; set; }
        public virtual ICollection<SupplierReview> SupplierReview { get; set; }
        public virtual ICollection<SupplierProperties> SupplierProperties { get; set; }
        public virtual ICollection<SupplierPackagesLog> SupplierPackagesLog { get; set; }
    }
}
