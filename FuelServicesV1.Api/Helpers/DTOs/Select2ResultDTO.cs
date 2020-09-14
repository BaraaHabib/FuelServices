using System.Collections.Generic;

namespace FuelServices.Api.Helpers.DTOs
{
    public class Select2ResultDTO
    {
        public string id { get; set; }

        public string text { get; set; }

        public List<Select2ResultDTO> children { get; set; }
    }
}