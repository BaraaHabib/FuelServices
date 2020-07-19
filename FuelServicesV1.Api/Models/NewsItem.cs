using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Models
{
    public class NewsItem
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsVisible { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AnchorText { get; set; }

        public string AnchorUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}
