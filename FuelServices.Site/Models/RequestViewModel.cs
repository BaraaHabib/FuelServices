using DBContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Models
{
    public class RequestViewModel
    {
        
        public Request Req { get; set; }


        [MinLength(1,ErrorMessage ="Select at least one supplier")]
        public List<int> SelectedOffers { get; set; }
    }
}
