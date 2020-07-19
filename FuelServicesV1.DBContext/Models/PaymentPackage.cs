using Elect.Web.DataTable.Attributes;
using FuelServices.DBContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBContext.Models
{
    public partial class PaymentPackage : BaseEntity
    {
        public PaymentPackage()
        {
            this.CustomerPackagesLogs = new HashSet<CustomerPackagesLog>();
            SupplierPackagesLogs = new HashSet<SupplierPackagesLog>();
            PaymentPackageFeature = new HashSet<PaymentPackageFeature>();
        }


        [Required]
        [DataTable(DisplayName = "Name", Order = 2)]
        public string Name { get; set; }

        [Required]
        [DataTable(DisplayName = "Display Name", Order = 3)]
        [Display(Name= "Display Name", Order = 3)]
        public string DisplayName { get; set; }

        [DataTable(DisplayName = "Description", Order = 4)]
        [Display(Name = "Description", Order = 4)]
        public string Description { get; set; }

        [Required]
        [DataTable(DisplayName = "Period", Order = 5)]
        public int? Period { get; set; }

        [DataTable(DisplayName = "Image Url", Order = 6)]
        [Display(Name = "Image", Order = 6)]
        public string ImageUrl { get; set; }

        [Required]
        [DataTable(DisplayName = "Price", Order = 7)]
        public double Price { get; set; }

        [Required]
        [DataTable(DisplayName = "Price Unit", Order = 8)]
        [Display(Name = "Price Unit", Order = 8)]
        public string PriceUnit { get; set; }

        [DataTable(DisplayName = "Discount", Order = 9)]
        public double? Discount { get; set; }

        [DataTable(DisplayName = "Discount Type", Order = 10)]
        [Display(Name= "Discount Type", Order = 10)]
        public DiscountType DiscountType { get; set; }

        [DataTable(DisplayName = "Discount Unit", Order = 11)]
        [Display(Name = "Discount Unit", Order = 4)]
        public string DiscountUnit { get; set; }

        [Required]
        [DataTable(DisplayName = "Level", Order = 12)]
        [Display(Name = "Level", Order = 4)]
        public int? ItemLevel { get; set; }

        [Required]
        [DataTable(DisplayName = "Main Color", Order = 14)]
        [Display(Name = "Main Color", Order = 4)]
        public string MainColor { get; set; }

        [Required]
        [DataTable(DisplayName = "Type", Order = 15)]
        public PackageType Type { get; set; }

        [DataTable(DisplayName = "Valid", Order = 16)]
        public bool IsValid { get; set; }

        public virtual ICollection<CustomerPackagesLog> CustomerPackagesLogs { get; set; }
        public virtual ICollection<SupplierPackagesLog> SupplierPackagesLogs { get; set; }
        public virtual ICollection<PaymentPackageFeature> PaymentPackageFeature { get; set; }
    }

    public enum PackageType : int
    {
        SupplierPackage = 0,
        CustomerPackage = 1
    }

    public enum DiscountType : int
    {
        None = 0,
        Percentage = 1,
        Fixed = 2
    }
}
