namespace FuelServices.DBContext.Domain
{
    public partial class SearchTermViewModel : BaseDomain
    {

        public string Keyword { get; set; }

        public int? Count { get; set; }
    }
}
