using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Helpers.Configurations
{
    public class MyConfiguration
    {
        public bool? ExceptionMessage { get; set; }
        public string SuperAdminEmail { get; set; }

        public string EmailSenderDisplayName { get; set; }
        public string EmailHost { get; set; }
        public string EmailPort { get; set; }
        public string EmailEnableSsl { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }


        public string CustomerConfirmationTimeOutInHours { get; set; }


        // jwt
        public string Secret { get; set; }
    }
}
