using DBContext.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.Site.Models
{
    public class RequestViewModel
    {
        public Request Req { get; set; }

        [MinLength(1, ErrorMessage = "Select at least one supplier")]
        public List<int> SelectedOffers { get; set; }
    }
}