using Elect.Web.DataTable.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBContext.Models
{
    public partial class ContentManagement : BaseEntity
    {
       
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsVisible { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AnchorText { get; set; }

        public string AnchorUrl { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }
    }
}
