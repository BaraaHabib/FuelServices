using AngleSharp.Io.Cookie;
using AutoMapper;
using DBContext.Models;
using FuelServices.DBContext.DatatablesModels;
using FuelServices.DBContext.Domain;
using System.Linq;
using X.PagedList;

namespace DBContext.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ContactUs, ContactUsViewModel>()
              .ForMember(dest => dest.FullName, act => act.MapFrom(x => $"{x.FirstName} {x.LastName}:"));

            CreateMap<OfferDatatableViewModel, Offer>();
            CreateMap<Offer, OfferDatatableViewModel>();
            CreateMap<IPagedList<Offer>, IPagedList<OfferDatatableViewModel>>().ConvertUsing<PagedListConverter<Offer, OfferDatatableViewModel>>();
            CreateMap<IPagedList<OfferDatatableViewModel>, IPagedList<Offer>>().ConvertUsing<PagedListConverter<OfferDatatableViewModel, Offer>>();

            CreateMap<AdvertisementViewModel, Advertisement>();
            CreateMap<Advertisement, AdvertisementViewModel>();
            CreateMap<IPagedList<Advertisement>, IPagedList<AdvertisementViewModel>>().ConvertUsing<PagedListConverter<Advertisement, AdvertisementViewModel>>();
            CreateMap<IPagedList<AdvertisementViewModel>, IPagedList<Advertisement>>().ConvertUsing<PagedListConverter<AdvertisementViewModel, Advertisement>>();

            CreateMap<AdvertisementCategoryViewModel, AdvertisementCategory>();
            CreateMap<AdvertisementCategory, AdvertisementCategoryViewModel>();
            CreateMap<IPagedList<AdvertisementCategory>, IPagedList<AdvertisementCategoryViewModel>>().ConvertUsing<PagedListConverter<AdvertisementCategory, AdvertisementCategoryViewModel>>();
            CreateMap<IPagedList<AdvertisementCategoryViewModel>, IPagedList<AdvertisementCategory>>().ConvertUsing<PagedListConverter<AdvertisementCategoryViewModel, AdvertisementCategory>>();

            CreateMap<AdvertisementOwnerViewModel, AdvertisementOwner>();
            CreateMap<AdvertisementOwner, AdvertisementOwnerViewModel>();
            CreateMap<IPagedList<AdvertisementOwner>, IPagedList<AdvertisementOwnerViewModel>>().ConvertUsing<PagedListConverter<AdvertisementOwner, AdvertisementOwnerViewModel>>();
            CreateMap<IPagedList<AdvertisementOwnerViewModel>, IPagedList<AdvertisementOwner>>().ConvertUsing<PagedListConverter<AdvertisementOwnerViewModel, AdvertisementOwner>>();

            CreateMap<CityViewModel, City>();
            CreateMap<City, CityViewModel>();
            CreateMap<IPagedList<City>, IPagedList<CityViewModel>>().ConvertUsing<PagedListConverter<City, CityViewModel>>();
            CreateMap<IPagedList<CityViewModel>, IPagedList<City>>().ConvertUsing<PagedListConverter<CityViewModel, City>>();

            CreateMap<AdvertisementPropertyViewModel, AdvertisementProperty>();
            CreateMap<AdvertisementProperty, AdvertisementPropertyViewModel>();
            CreateMap<IPagedList<AdvertisementProperty>, IPagedList<AdvertisementPropertyViewModel>>().ConvertUsing<PagedListConverter<AdvertisementProperty, AdvertisementPropertyViewModel>>();
            CreateMap<IPagedList<AdvertisementPropertyViewModel>, IPagedList<AdvertisementProperty>>().ConvertUsing<PagedListConverter<AdvertisementPropertyViewModel, AdvertisementProperty>>();

            CreateMap<AdvertisementTypeViewModel, AdvertisementType>();
            CreateMap<AdvertisementType, AdvertisementTypeViewModel>();
            CreateMap<IPagedList<AdvertisementType>, IPagedList<AdvertisementTypeViewModel>>().ConvertUsing<PagedListConverter<AdvertisementType, AdvertisementTypeViewModel>>();
            CreateMap<IPagedList<AdvertisementTypeViewModel>, IPagedList<AdvertisementType>>().ConvertUsing<PagedListConverter<AdvertisementTypeViewModel, AdvertisementType>>();

            CreateMap<AdvertisementTypePropertyViewModel, AdvertisementTypeProperty>();
            CreateMap<AdvertisementTypeProperty, AdvertisementTypePropertyViewModel>();
            CreateMap<IPagedList<AdvertisementTypeProperty>, IPagedList<AdvertisementTypePropertyViewModel>>().ConvertUsing<PagedListConverter<AdvertisementTypeProperty, AdvertisementTypePropertyViewModel>>();
            CreateMap<IPagedList<AdvertisementTypePropertyViewModel>, IPagedList<AdvertisementTypeProperty>>().ConvertUsing<PagedListConverter<AdvertisementTypePropertyViewModel, AdvertisementTypeProperty>>();

            CreateMap<AirportViewModel, Airport>();
            CreateMap<Airport, AirportViewModel>();
            CreateMap<IPagedList<Airport>, IPagedList<AirportViewModel>>().ConvertUsing<PagedListConverter<Airport, AirportViewModel>>();
            CreateMap<IPagedList<AirportViewModel>, IPagedList<Airport>>().ConvertUsing<PagedListConverter<AirportViewModel, Airport>>();

            CreateMap<ContactUsViewModel, ContactUs>();
            CreateMap<ContactUs, ContactUsViewModel>();
            CreateMap<IPagedList<ContactUs>, IPagedList<ContactUsViewModel>>().ConvertUsing<PagedListConverter<ContactUs, ContactUsViewModel>>();
            CreateMap<IPagedList<ContactUsViewModel>, IPagedList<ContactUs>>().ConvertUsing<PagedListConverter<ContactUsViewModel, ContactUs>>();

            CreateMap<AirportPropertyViewModel, AirportProperty>();
            CreateMap<AirportProperty, AirportPropertyViewModel>();
            CreateMap<IPagedList<AirportProperty>, IPagedList<AirportPropertyViewModel>>().ConvertUsing<PagedListConverter<AirportProperty, AirportPropertyViewModel>>();
            CreateMap<IPagedList<AirportPropertyViewModel>, IPagedList<AirportProperty>>().ConvertUsing<PagedListConverter<AirportPropertyViewModel, AirportProperty>>();

            CreateMap<ContentManagementViewModel, ContentManagement>();
            CreateMap<ContentManagement, ContentManagementViewModel>();
            CreateMap<IPagedList<ContentManagement>, IPagedList<ContentManagementViewModel>>().ConvertUsing<PagedListConverter<ContentManagement, ContentManagementViewModel>>();
            CreateMap<IPagedList<ContentManagementViewModel>, IPagedList<ContentManagement>>().ConvertUsing<PagedListConverter<ContentManagementViewModel, ContentManagement>>();

            CreateMap<ContinentViewModel, Continent>();
            CreateMap<Continent, ContinentViewModel>();
            CreateMap<IPagedList<Continent>, IPagedList<ContinentViewModel>>().ConvertUsing<PagedListConverter<Continent, ContinentViewModel>>();
            CreateMap<IPagedList<ContinentViewModel>, IPagedList<Continent>>().ConvertUsing<PagedListConverter<ContinentViewModel, Continent>>();

            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, CountryViewModel>();
            CreateMap<IPagedList<Country>, IPagedList<CountryViewModel>>().ConvertUsing<PagedListConverter<Country, CountryViewModel>>();
            CreateMap<IPagedList<CountryViewModel>, IPagedList<Country>>().ConvertUsing<PagedListConverter<CountryViewModel, Country>>();

            CreateMap<CustomerViewModel, Customer>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<IPagedList<Customer>, IPagedList<CustomerViewModel>>().ConvertUsing<PagedListConverter<Customer, CustomerViewModel>>();
            CreateMap<IPagedList<CustomerViewModel>, IPagedList<Customer>>().ConvertUsing<PagedListConverter<CustomerViewModel, Customer>>();

            CreateMap<CustomerPackagesLogViewModel, CustomerPackagesLog>();
            CreateMap<CustomerPackagesLog, CustomerPackagesLogViewModel>();
            CreateMap<IPagedList<CustomerPackagesLog>, IPagedList<CustomerPackagesLogViewModel>>().ConvertUsing<PagedListConverter<CustomerPackagesLog, CustomerPackagesLogViewModel>>();
            CreateMap<IPagedList<CustomerPackagesLogViewModel>, IPagedList<CustomerPackagesLog>>().ConvertUsing<PagedListConverter<CustomerPackagesLogViewModel, CustomerPackagesLog>>();

            CreateMap<EmailBodyDefaultParamsViewModel, EmailBodyDefaultParams>();
            CreateMap<EmailBodyDefaultParams, EmailBodyDefaultParamsViewModel>();
            CreateMap<IPagedList<EmailBodyDefaultParams>, IPagedList<EmailBodyDefaultParamsViewModel>>().ConvertUsing<PagedListConverter<EmailBodyDefaultParams, EmailBodyDefaultParamsViewModel>>();
            CreateMap<IPagedList<EmailBodyDefaultParamsViewModel>, IPagedList<EmailBodyDefaultParams>>().ConvertUsing<PagedListConverter<EmailBodyDefaultParamsViewModel, EmailBodyDefaultParams>>();

            CreateMap<FeatureViewModel, Feature>();
            CreateMap<Feature, FeatureViewModel>();
            CreateMap<IPagedList<Feature>, IPagedList<FeatureViewModel>>().ConvertUsing<PagedListConverter<Feature, FeatureViewModel>>();
            CreateMap<IPagedList<FeatureViewModel>, IPagedList<Feature>>().ConvertUsing<PagedListConverter<FeatureViewModel, Feature>>();

            CreateMap<LogViewModel, Log>();
            CreateMap<Log, LogViewModel>();
            CreateMap<IPagedList<Log>, IPagedList<LogViewModel>>().ConvertUsing<PagedListConverter<Log, LogViewModel>>();
            CreateMap<IPagedList<LogViewModel>, IPagedList<Log>>().ConvertUsing<PagedListConverter<LogViewModel, Log>>();

            #region Pachages Mappings

            CreateMap<PaymentPackageViewModel, PaymentPackage>();
            CreateMap<PaymentPackage, PaymentPackageViewModel>();
            CreateMap<IPagedList<PaymentPackage>, IPagedList<PaymentPackageViewModel>>().ConvertUsing<PagedListConverter<PaymentPackage, PaymentPackageViewModel>>();
            CreateMap<IPagedList<PaymentPackageViewModel>, IPagedList<PaymentPackage>>().ConvertUsing<PagedListConverter<PaymentPackageViewModel, PaymentPackage>>();

            CreateMap<PaymentPackageFeatureViewModel, PaymentPackageFeature>();
            CreateMap<PaymentPackageFeature, PaymentPackageFeatureViewModel>();
            CreateMap<IPagedList<PaymentPackageFeature>, IPagedList<PaymentPackageFeatureViewModel>>().ConvertUsing<PagedListConverter<PaymentPackageFeature, PaymentPackageFeatureViewModel>>();
            CreateMap<IPagedList<PaymentPackageFeatureViewModel>, IPagedList<PaymentPackageFeature>>().ConvertUsing<PagedListConverter<PaymentPackageFeatureViewModel, PaymentPackageFeature>>();

            #endregion Pachages Mappings

            CreateMap<PropertyViewModel, Property>();
            CreateMap<Property, PropertyViewModel>();
            CreateMap<IPagedList<Property>, IPagedList<PropertyViewModel>>().ConvertUsing<PagedListConverter<Property, PropertyViewModel>>();
            CreateMap<IPagedList<PropertyViewModel>, IPagedList<Property>>().ConvertUsing<PagedListConverter<PropertyViewModel, Property>>();

            CreateMap<SupplierViewModel, FuelSupplier>();
            CreateMap<FuelSupplier, SupplierViewModel>();
            CreateMap<IPagedList<FuelSupplier>, IPagedList<SupplierViewModel>>().ConvertUsing<PagedListConverter<FuelSupplier, SupplierViewModel>>();
            CreateMap<IPagedList<SupplierViewModel>, IPagedList<FuelSupplier>>().ConvertUsing<PagedListConverter<SupplierViewModel, FuelSupplier>>();

            CreateMap<SupplierContactViewModel, SupplierContact>();
            CreateMap<SupplierContact, SupplierContactViewModel>();
            CreateMap<IPagedList<SupplierContact>, IPagedList<SupplierContactViewModel>>().ConvertUsing<PagedListConverter<SupplierContact, SupplierContactViewModel>>();
            CreateMap<IPagedList<SupplierContactViewModel>, IPagedList<SupplierContact>>().ConvertUsing<PagedListConverter<SupplierContactViewModel, SupplierContact>>();

            CreateMap<SupplierContactPersonViewModel, SupplierContactPerson>();
            CreateMap<SupplierContactPerson, SupplierContactPersonViewModel>();
            CreateMap<IPagedList<SupplierContactPerson>, IPagedList<SupplierContactPersonViewModel>>().ConvertUsing<PagedListConverter<SupplierContactPerson, SupplierContactPersonViewModel>>();
            CreateMap<IPagedList<SupplierContactPersonViewModel>, IPagedList<SupplierContactPerson>>().ConvertUsing<PagedListConverter<SupplierContactPersonViewModel, SupplierContactPerson>>();

            CreateMap<SupplierContactPersonContactViewModel, SupplierContactPersonContact>();
            CreateMap<SupplierContactPersonContact, SupplierContactPersonContactViewModel>();
            CreateMap<IPagedList<SupplierContactPersonContact>, IPagedList<SupplierContactPersonContactViewModel>>().ConvertUsing<PagedListConverter<SupplierContactPersonContact, SupplierContactPersonContactViewModel>>();
            CreateMap<IPagedList<SupplierContactPersonContactViewModel>, IPagedList<SupplierContactPersonContact>>().ConvertUsing<PagedListConverter<SupplierContactPersonContactViewModel, SupplierContactPersonContact>>();

            //CreateMap<SupplierServicePropertyViewModel, SupplierServiceProperty>();
            //CreateMap<SupplierServiceProperty, SupplierServicePropertyViewModel>();
            //CreateMap<IPagedList<SupplierServiceProperty>, IPagedList<SupplierServicePropertyViewModel>>().ConvertUsing<PagedListConverter<SupplierServiceProperty, SupplierServicePropertyViewModel>>();
            //CreateMap<IPagedList<SupplierServicePropertyViewModel>, IPagedList<SupplierServiceProperty>>().ConvertUsing<PagedListConverter<SupplierServicePropertyViewModel, SupplierServiceProperty>>();

            CreateMap<SupplierTypeViewModel, SupplierType>();
            CreateMap<SupplierType, SupplierTypeViewModel>();
            CreateMap<IPagedList<SupplierType>, IPagedList<SupplierTypeViewModel>>().ConvertUsing<PagedListConverter<SupplierType, SupplierTypeViewModel>>();
            CreateMap<IPagedList<SupplierTypeViewModel>, IPagedList<SupplierType>>().ConvertUsing<PagedListConverter<SupplierTypeViewModel, SupplierType>>();

            #region Contacts Mapping

            CreateMap<SupplierContact, ContactRecordModel>()
                .ReverseMap();

            CreateMap<SupplierContactPersonContact, ContactPersonValueModel>()
                .ReverseMap()
                ;

            CreateMap<SupplierContactPerson, SupplierContactPersonRecordModel>()
               .ForMember(dist => dist.Values, x => x.MapFrom(src => src.SupplierContactPersonContact))
               .ReverseMap()
               ;

            #endregion Contacts Mapping
            //CreateMissingTypeMaps = true;
            //ValidateInlineMaps = true;
        }
    }
}