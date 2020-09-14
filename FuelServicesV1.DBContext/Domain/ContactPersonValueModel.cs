using DBContext.Models;

namespace FuelServices.DBContext.Domain
{
    public class ContactPersonValueModel : BaseDomain
    {
        public string Value { get; set; }

        public string Description { get; set; }

        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}