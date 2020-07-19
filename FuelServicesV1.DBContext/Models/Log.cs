using Elect.Web.DataTable.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBContext.Models
{
    public partial class Log 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [DataTable(DisplayName = "Message", Order = 2)]
        public string Message { get; set; }

        [DataTable(DisplayName = "Message Template", Order = 3)]
        public string MessageTemplate { get; set; }

        [DataTable(DisplayName = "Level", Order = 4)]
        public string Level { get; set; }

        [Required]
        [DataTable(DisplayName = "TimeStamp", Order = 5)]
        public DateTimeOffset TimeStamp { get; set; }

        [DataTable(DisplayName = "Exception", Order = 6)]
        public string Exception { get; set; }

        [DataTable(DisplayName = "Properties", Order = 7)]
        public string Properties { get; set; }

        [DataTable(DisplayName = "Log Event", Order = 8)]
        public string LogEvent { get; set; }
    }
}
