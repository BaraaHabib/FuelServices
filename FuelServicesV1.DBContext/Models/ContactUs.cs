using Elect.Web.DataTable.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class ContactUs : BaseEntity
    {
        public ContactUs()
        {
        }

        [DataTable(IsVisible = false, Order = 2)]
        public int? CustomerId { get; set; }

        [Required]
        [DataTable(DisplayName = "First Name", Order = 3)]
        public string FirstName { get; set; }

        [Required]
        [DataTable(DisplayName = "Last Name", Order = 4)]
        public string LastName { get; set; }

        [Required]
        [DataTable(DisplayName = "Email", Order = 5)]
        public string Email { get; set; }

        [Required]
        [DataTable(DisplayName = "Tel", Order = 6)]
        public string Tel { get; set; }

        [Required]
        [DataTable(DisplayName = "Subject", Order = 7)]
        public string Subject { get; set; }

        [Required]
        [DataTable(DisplayName = "Message", Order = 8)]
        public string Message { get; set; }

        [Required]
        [DataTable(DisplayName = "Submit Date", Order = 9)]
        public DateTime SubmitDate { get; set; }

        public bool IsRead { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
