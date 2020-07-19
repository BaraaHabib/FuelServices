using Elect.Web.DataTable.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class Continent : BaseEntity
    {
        public Continent()
        {
            Country = new HashSet<Country>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        public virtual ICollection<Country> Country { get; set; }
        public string Code { get; set; }
    }
}
