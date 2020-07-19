
using System;

namespace FuelServices.DBContext.Domain
{
    public partial class NewsViewModel : BaseDomain
    {
        public string Title { get; set; }

        public string Brief { get; set; }

        public string Author { get; set; }

        public string Script { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
