using Elect.Web.DataTable.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class EmailBodyDefaultParams : BaseEntity
    {

        [DataTable(DisplayName = "Email Type", Order = 2)]
        public string EmailTypeName { get; set; }

        [DataTable(DisplayName = "Title 1", Order = 3)]
        public string Title1 { get; set; }

        [DataTable(DisplayName = "Title 2", Order = 4)]
        public string Title2 { get; set; }

        [DataTable(DisplayName = "Title 3", Order = 5)]
        public string Title3 { get; set; }

        [DataTable(DisplayName = "Background Color", Order = 6)]
        public string BackgroundColor { get; set; }

        [DataTable(DisplayName = "Card Color", Order = 7)]
        public string CardColor { get; set; }

        [DataTable(DisplayName = "Font Color", Order = 7)]
        public string FontColor { get; set; }

        [DataTable(DisplayName = "Logo", Order = 8)]
        public string Logo { get; set; }

        [DataTable(DisplayName = "Banner", Order = 9)]
        public string Banner { get; set; }

        [DataTable(DisplayName = "Contact Email", Order = 10)]
        public string ContactEmail { get; set; }

        [DataTable(DisplayName = "Action Url", Order = 11)]
        public string CallbackUrl { get; set; }

        [DataTable(DisplayName = "Site Url", Order = 12)]
        public string SiteUrl { get; set; }

        [DataTable(DisplayName = "Template", Order = 13)]
        public string TemplateUrl { get; set; }

        [DataTable(DisplayName = "Action Url", Order = 16)]
        public string CallbackDisplayText { get; set; }

        [DataTable(DisplayName = "Email Caption", Order = 17)]
        public string EmailCaption { get; set; }
    }
}
