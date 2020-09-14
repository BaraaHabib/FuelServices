namespace FuelServices.DBContext.Domain
{
    public partial class CurrencyViewModel : BaseDomain
    {
        public string Entity { get; set; }
        public string CurrencyValue { get; set; }
        public string AlphabeticCode { get; set; }
        public string NumericCode { get; set; }
        public string MinorUnit { get; set; }
    }
}