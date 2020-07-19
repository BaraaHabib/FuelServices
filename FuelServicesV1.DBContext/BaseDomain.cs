using System;
using System.Collections.Generic;
using System.Text;

namespace FuelServices.DBContext
{
    public class BaseDomain
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }
        public int ItemOrder { get; set; }
    }
}
