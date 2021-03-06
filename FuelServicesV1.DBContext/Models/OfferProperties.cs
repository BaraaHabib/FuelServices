﻿using Elect.Web.DataTable.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class OfferProperties : BaseEntity
    {
        
        [Required]
        [DataTable(IsVisible = false, Order = 2)]
        public int? PropertyId { get; set; }

        [Required]
        [DataTable(IsVisible = false, Order = 3)]
        public int? OfferId { get; set; }

        [DataTable(DisplayName = "Offer Property", Order = 4)]
        public string Value { get; set; }

        [DataTable(DisplayName = "Unit", Order = 4)]
        public string Unit { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Property Property { get; set; }
    }
}
