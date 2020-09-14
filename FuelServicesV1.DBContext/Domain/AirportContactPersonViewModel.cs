namespace FuelServices.DBContext.Domain
{
    public partial class AirportContactPersonViewModel : BaseDomain
    {
        public int? AirportId { get; set; }

        public string Name { get; set; }

        public string JobTitle { get; set; }
    }
}