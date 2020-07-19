using DBContext.Models;
using FuelServices.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuelServices.Site.Models
{
    public class StatisticsHelper
    {
        private readonly AirportCoreContext _context;

        public StatisticsHelper()
        {
        }

        public StatisticsHelper(AirportCoreContext context)
        {
            _context = context;
        }

        //public ModelStatistics CalculateCustomers()
        //{
        //    ModelStatistics model = new ModelStatistics();
        //    model.Title = "Customers";
        //    model.NumberOfXThisMonth = _context.Customer.Where(c => c.Created.Month == DateTime.Now.Month).Count();
        //    model.NumberOfAllX = _context.Customer.Count();
        //    if (model.NumberOfAllX > 0)
        //        model.IncremntPercentage = (model.NumberOfXThisMonth * 100) / model.NumberOfAllX;
        //    else
        //        model.IncremntPercentage = 0;
        //    model.IncremntPercentageString = model.IncremntPercentage + "%";
        //    return model;
        //}

        public ModelStatistics CalculateSubscription()
        {
            ModelStatistics model = new ModelStatistics();
            model.Title = "Subscriptions";
            model.NumberOfXThisMonth = _context.CustomerPackagesLog.Where(c => ((DateTime)c.StartDate).Month == DateTime.Now.Month
            && c.PaymentPackage.Type == PackageType.CustomerPackage).Count();
            model.NumberOfAllX = _context.CustomerPackagesLog.Where(c => c.PaymentPackage.Type == PackageType.CustomerPackage).Count();
            if (model.NumberOfAllX > 0)
                model.IncremntPercentage = (model.NumberOfXThisMonth * 100) / model.NumberOfAllX;
            else
                model.IncremntPercentage = 0;
            model.IncremntPercentageString = model.IncremntPercentage + "%";
            return model;
        }

        //public ModelStatistics CalculateAds()
        //{
        //    ModelStatistics model = new ModelStatistics();
        //    model.Title = "Advertisments";
        //    var ads = _context.Advertisement.Where(c => ((DateTime)c.PublishDate).Month == DateTime.Now.Month).ToList();
        //    model.NumberOfXThisMonth = ads.Count();
        //    model.Revenue = 0;
        //    foreach (var item in ads)
        //    {
        //        model.Revenue += (double)item.TotalPrice;
        //    }
        //    model.RevenueString = model.Revenue + " $";
        //    var dd = _context.Advertisement.ToList();
        //    model.NumberOfAllX = _context.Advertisement.Count();
        //    if (model.NumberOfAllX > 0)
        //        model.IncremntPercentage = (model.NumberOfXThisMonth * 100) / model.NumberOfAllX;
        //    else
        //        model.IncremntPercentage = 0;
        //    model.IncremntPercentageString = model.IncremntPercentage + "%";
        //    return model;
        //}

        //public List<ModelStatistics> CalculateAdsDetails()
        //{
        //    List<ModelStatistics> models = new List<ModelStatistics>();
        //    foreach (var item in _context.Advertisement.OrderByDescending(a => ((DateTime)a.PublishDate).Month).ToList())
        //    {
        //        ModelStatistics model = new ModelStatistics();
        //        model.Title = item.Title;
        //        model.Type = item.AdvertisementImageType.Name;
        //        model.NumberOfAllX = (int)item.CaptionClicks;
        //        models.Add(model);
        //    }
        //    return models;
        //}

        public ModelStatistics CalculateSuppliers()
        {
            ModelStatistics model = new ModelStatistics();
            model.Title = "Suppliers";
            model.NumberOfXThisMonth = _context.CustomerPackagesLog.Where(c => ((DateTime)c.StartDate).Month == DateTime.Now.Month
            && c.PaymentPackage.Type == PackageType.SupplierPackage).Count();
            model.NumberOfAllX = _context.CustomerPackagesLog.Where(c => c.PaymentPackage.Type == PackageType.SupplierPackage).Count();
            if (model.NumberOfAllX > 0)
                model.IncremntPercentage = (model.NumberOfXThisMonth * 100) / model.NumberOfAllX;
            else
                model.IncremntPercentage = 0;
            model.IncremntPercentageString = model.IncremntPercentage + "%";
            return model;
        }

        //public List<ModelStatistics> CalculatePaymentPackages()
        //{
        //    List<ModelStatistics> models = new List<ModelStatistics>();
        //    foreach (var item in _context.PaymentPackage)
        //    {
        //        ModelStatistics model = new ModelStatistics();
        //        model.Title = item.DisplayName;
        //        model.NumberOfXThisMonth = _context.CustomerPackagesLog.Where(c => ((DateTime)c.StartDate).Month == DateTime.Now.Month
        //        && c.PaymentPackageId == item.Id).Count();
        //        model.NumberOfAllX = _context.CustomerPackagesLog.Where(c => c.PaymentPackageId == item.Id).Count();
        //        if (model.NumberOfAllX > 0)
        //            model.IncremntPercentage = (model.NumberOfXThisMonth * 100) / model.NumberOfAllX;
        //        else
        //            model.IncremntPercentage = 0;
        //        model.IncremntPercentageString = model.IncremntPercentage + "%";
        //        model.ColorPalette = item.ColorPalette;
        //        model.Type = (item.Type == PackageType.CustomerPackage) ? "Customer Package" : "Supplier Package";
        //        models.Add(model);
        //    }
        //    return models;
        //}

        //public List<ModelStatistics> CalculateServices()
        //{
        //    List<ModelStatistics> models = new List<ModelStatistics>();
        //    foreach (var item in _context.MainCategory)
        //    {
        //        ModelStatistics model = new ModelStatistics();
        //        model.Title = item.DisplayName;
        //        model.NumberOfXThisMonth = 0;
        //        foreach (var subItem in item.SubCategory)
        //        {
        //            model.NumberOfXThisMonth += subItem.CategorySupplier.Select(sc => sc.Supplier).Distinct().Count();
        //        }
        //        model.NumberOfAllX = _context.Customer.Count();
        //        if (model.NumberOfAllX > 0)
        //            model.IncremntPercentage = (model.NumberOfXThisMonth * 100) / model.NumberOfAllX;
        //        else
        //            model.IncremntPercentage = 0;
        //        model.IncremntPercentageString = model.IncremntPercentage + "%";
        //    }
        //    return models;
        //}

    }
}
