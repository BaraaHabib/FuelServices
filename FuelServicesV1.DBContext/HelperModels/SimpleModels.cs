namespace FuelServices.DBContext.HelperModels
{
    public class SimpleIdValueAPIModels
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }

    public class SimpleIdValueDescAPIModels : SimpleIdValueAPIModels
    {
        public string Description { get; set; }
    }
}