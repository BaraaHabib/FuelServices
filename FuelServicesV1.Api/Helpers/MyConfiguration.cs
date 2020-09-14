using System;

namespace FuelServices.Api.Helpers
{
    public class MyConfiguration
    {
        public bool? ExceptionMessage { get; set; }
        public string SuperAdminEmail { get; set; }

        #region Email Configuration

        public string EmailSenderDisplayName { get; set; }
        public string EmailHost { get; set; }
        public string EmailPort { get; set; }
        public string EmailEnableSsl { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }

        #endregion

        public string CustomerConfirmationTimeOutInHours { get; internal set; }

        // jwt
        public string Secret { get; set; }
    }
}