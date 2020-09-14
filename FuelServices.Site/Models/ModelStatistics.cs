namespace FuelServices.Site.Models
{
    public class ModelStatistics
    {
        public string Title { get; set; }
        public int NumberOfXThisMonth { get; set; }
        public int NumberOfAllX { get; set; }
        public double IncremntPercentage { get; set; }
        public string IncremntPercentageString { get; set; }
        public double Revenue { get; set; }
        public string RevenueString { get; set; }

        //public ColorPalette ColorPalette { get; set; }
        public string Type { get; set; }
    }
}