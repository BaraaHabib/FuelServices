
using System;

namespace FuelServices.DBContext.Domain
{
    public partial class ContactUsViewModel : BaseDomain
    {
        public ContactUsViewModel()
        {
        }

        public int? CustomerId { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime? SubmitDate { get; set; }
        
        public virtual CustomerViewModel Customer { get; set; }
    }
}
