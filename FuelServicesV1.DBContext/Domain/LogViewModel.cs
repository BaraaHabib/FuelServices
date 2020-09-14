using System;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.DBContext.Domain
{
    public partial class LogViewModel : BaseDomain
    {
        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        [Required]
        public DateTimeOffset TimeStamp { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }

        public string LogEvent { get; set; }
    }
}