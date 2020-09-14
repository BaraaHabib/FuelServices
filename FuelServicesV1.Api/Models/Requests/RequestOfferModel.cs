using DBContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Models.Requests
{
    public class RequestOfferModel
    {
        public string Supplier { get; set; }

        public int RequestOfferId { get; set; }

        public ReplyStatus Status { get; set; }

        public string StatusText { get; set; }

    }
}
