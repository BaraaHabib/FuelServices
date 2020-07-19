using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            ContactUs = new HashSet<ContactUs>();
            Request = new HashSet<Request>();
            SupplierReview = new HashSet<SupplierReview>();
            CustomerPackagesLog = new HashSet<CustomerPackagesLog>();
        }


        [DataTable(IsVisible = false, Order = 3)]
        public string UserId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name ="Image")]
        public string ImageUrl { get; set; }

        [DataTable(IsVisible = false, Order = 7)]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ContactUs> ContactUs { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<SupplierReview> SupplierReview { get; set; }
        public virtual ICollection<CustomerPackagesLog> CustomerPackagesLog { get; set; }
    }
}
