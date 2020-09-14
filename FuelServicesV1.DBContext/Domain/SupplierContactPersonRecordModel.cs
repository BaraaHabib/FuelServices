using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuelServices.DBContext.Domain
{
    public class SupplierContactPersonRecordModel : BaseDomain
    {
        public SupplierContactPersonRecordModel()
        {
            Values = new List<ContactPersonValueModel>();
        }
        
        [Required]
        public string Name { get; set; }

        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }

        public virtual List<ContactPersonValueModel> Values { get; set; }
    }
}