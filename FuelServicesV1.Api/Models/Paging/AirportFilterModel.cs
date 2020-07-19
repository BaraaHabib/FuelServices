using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Models.Paging
{
    public class AirportFilterModel : FilterModelBase
    {
        public string Term { get; set; }

        public AirportFilterModel() : base()
        {
            this.Limit = 10;
        }


        public override object Clone()
        {
            var jsonString = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject(jsonString, this.GetType());
        }
    }
}
