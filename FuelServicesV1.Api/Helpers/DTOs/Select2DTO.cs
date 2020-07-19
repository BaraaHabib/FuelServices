using System.Collections.Generic;

namespace FuelServices.Api.Helpers.DTOs
{
    public class Select2DTO
    {
        public List<Select2ResultDTO> results { get; set; }

        public Select2PaginateDTO paginate { get; set; }
    }
}
