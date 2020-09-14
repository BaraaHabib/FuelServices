namespace FuelServices.DBContext.DatatablesModels
{
    public class OfferDatatableViewModel : BaseDomain
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public string DuesTaxesLevies { get; set; }
    }
}