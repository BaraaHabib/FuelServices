namespace FuelServices.DBContext.Domain
{
    public partial class ContentManagementViewModel : BaseDomain
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool? IsVisible { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AnchorText { get; set; }

        public string AnchorUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}