namespace FuelServices.DBContext.Domain
{
    public partial class AirportPropertyViewModel : BaseDomain
    {
        public int? AirportId { get; set; }

        public int? PropertyId { get; set; }

        public string Value { get; set; }


        public string Unit { get; set; }

        public virtual PropertyViewModel Property { get; set; }
    }
}
