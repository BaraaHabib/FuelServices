using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Models
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
