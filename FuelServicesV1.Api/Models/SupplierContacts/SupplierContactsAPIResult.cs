using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FuelServices.Api.Models.SupplierContacts
{
    public class SupplierContactsAPIResult
    {
        public string Supplier { get; set; }

        public List<GlobalContact> GlobalContacts { get; set; }

        public List<PersonContact> PersonContacts { get; set; }

    }

    public class GlobalContact
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public string Order { get; set; }

        public string Description { get; set; }

    }

    public class PersonContact
    {

        public string Name { get; set; }

        public string JobTitle { get; set; }

        public string Order { get; set; }

    }
}
