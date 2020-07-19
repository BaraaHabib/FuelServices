using FuelServices.DBContext.Domain;
using DBContext.Models;
using AutoMapper;
using X.PagedList;
using FuelServices.DBContext.DatatablesModels;

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
              .ForMember(dest => dest.FullName, act => act.MapFrom(x => $"{x.FirstName} {x.LastName}:")) ;

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

            //CreateMap<AdvertisementImageTypeViewModel, AdvertisementImageType>();
            //CreateMap<AdvertisementImageType, AdvertisementImageTypeViewModel>();
            //CreateMap<IPagedList<AdvertisementImageType>, IPagedList<AdvertisementImageTypeViewModel>>().ConvertUsing<PagedListConverter<AdvertisementImageType, AdvertisementImageTypeViewModel>>();
            //CreateMap<IPagedList<AdvertisementImageTypeViewModel>, IPagedList<AdvertisementImageType>>().ConvertUsing<PagedListConverter<AdvertisementImageTypeViewModel, AdvertisementImageType>>();

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

            //CreateMap<AirportAdsViewModel, AirportAds>();
            //CreateMap<AirportAds, AirportAdsViewModel>();
            //CreateMap<IPagedList<AirportAds>, IPagedList<AirportAdsViewModel>>().ConvertUsing<PagedListConverter<AirportAds, AirportAdsViewModel>>();
            //CreateMap<IPagedList<AirportAdsViewModel>, IPagedList<AirportAds>>().ConvertUsing<PagedListConverter<AirportAdsViewModel, AirportAds>>();

            CreateMap<ContactUsViewModel, ContactUs>();
            CreateMap<ContactUs, ContactUsViewModel>();
            CreateMap<IPagedList<ContactUs>, IPagedList<ContactUsViewModel>>().ConvertUsing<PagedListConverter<ContactUs, ContactUsViewModel>>();
            CreateMap<IPagedList<ContactUsViewModel>, IPagedList<ContactUs>>().ConvertUsing<PagedListConverter<ContactUsViewModel, ContactUs>>();

            //CreateMap<AirportContactViewModel, AirportContact>();
            //CreateMap<AirportContact, AirportContactViewModel>();
            //CreateMap<IPagedList<AirportContact>, IPagedList<AirportContactViewModel>>().ConvertUsing<PagedListConverter<AirportContact, AirportContactViewModel>>();
            //CreateMap<IPagedList<AirportContactViewModel>, IPagedList<AirportContact>>().ConvertUsing<PagedListConverter<AirportContactViewModel, AirportContact>>();

            //CreateMap<AirportContactPersonViewModel, AirportContactPerson>();
            //CreateMap<AirportContactPerson, AirportContactPersonViewModel>();
            //CreateMap<IPagedList<AirportContactPerson>, IPagedList<AirportContactPersonViewModel>>().ConvertUsing<PagedListConverter<AirportContactPerson, AirportContactPersonViewModel>>();
            //CreateMap<IPagedList<AirportContactPersonViewModel>, IPagedList<AirportContactPerson>>().ConvertUsing<PagedListConverter<AirportContactPersonViewModel, AirportContactPerson>>();

            //CreateMap<AirportContactPersonContactViewModel, AirportContactPersonContact>();
            //CreateMap<AirportContactPersonContact, AirportContactPersonContactViewModel>();
            //CreateMap<IPagedList<AirportContactPersonContact>, IPagedList<AirportContactPersonContactViewModel>>().ConvertUsing<PagedListConverter<AirportContactPersonContact, AirportContactPersonContactViewModel>>();
            //CreateMap<IPagedList<AirportContactPersonContactViewModel>, IPagedList<AirportContactPersonContact>>().ConvertUsing<PagedListConverter<AirportContactPersonContactViewModel, AirportContactPersonContact>>();

            CreateMap<AirportPropertyViewModel, AirportProperty>();
            CreateMap<AirportProperty, AirportPropertyViewModel>();
            CreateMap<IPagedList<AirportProperty>, IPagedList<AirportPropertyViewModel>>().ConvertUsing<PagedListConverter<AirportProperty, AirportPropertyViewModel>>();
            CreateMap<IPagedList<AirportPropertyViewModel>, IPagedList<AirportProperty>>().ConvertUsing<PagedListConverter<AirportPropertyViewModel, AirportProperty>>();

            //CreateMap<CategorySupplierViewModel, CategorySupplier>();
            //CreateMap<CategorySupplier, CategorySupplierViewModel>();
            //CreateMap<IPagedList<CategorySupplier>, IPagedList<CategorySupplierViewModel>>().ConvertUsing<PagedListConverter<CategorySupplier, CategorySupplierViewModel>>();
            //CreateMap<IPagedList<CategorySupplierViewModel>, IPagedList<CategorySupplier>>().ConvertUsing<PagedListConverter<CategorySupplierViewModel, CategorySupplier>>();

            //CreateMap<ColorPaletteViewModel, ColorPalette>();
            //CreateMap<ColorPalette, ColorPaletteViewModel>();
            //CreateMap<IPagedList<ColorPalette>, IPagedList<ColorPaletteViewModel>>().ConvertUsing<PagedListConverter<ColorPalette, ColorPaletteViewModel>>();
            //CreateMap<IPagedList<ColorPaletteViewModel>, IPagedList<ColorPalette>>().ConvertUsing<PagedListConverter<ColorPaletteViewModel, ColorPalette>>();

            //CreateMap<CompanyViewModel, Company>();
            //CreateMap<Company, CompanyViewModel>();
            //CreateMap<IPagedList<Company>, IPagedList<CompanyViewModel>>().ConvertUsing<PagedListConverter<Company, CompanyViewModel>>();
            //CreateMap<IPagedList<CompanyViewModel>, IPagedList<Company>>().ConvertUsing<PagedListConverter<CompanyViewModel, Company>>();

            //CreateMap<CompanyContactViewModel, CompanyContact>();
            //CreateMap<CompanyContact, CompanyContactViewModel>();
            //CreateMap<IPagedList<CompanyContact>, IPagedList<CompanyContactViewModel>>().ConvertUsing<PagedListConverter<CompanyContact, CompanyContactViewModel>>();
            //CreateMap<IPagedList<CompanyContactViewModel>, IPagedList<CompanyContact>>().ConvertUsing<PagedListConverter<CompanyContactViewModel, CompanyContact>>();

        

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

            //CreateMap<CurrencyViewModel, Currency>();
            //CreateMap<Currency, CurrencyViewModel>();
            //CreateMap<IPagedList<Currency>, IPagedList<CurrencyViewModel>>().ConvertUsing<PagedListConverter<Currency, CurrencyViewModel>>();
            //CreateMap<IPagedList<CurrencyViewModel>, IPagedList<Currency>>().ConvertUsing<PagedListConverter<CurrencyViewModel, Currency>>();

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

            //CreateMap<MainCategoryViewModel, MainCategory>();
            //CreateMap<MainCategory, MainCategoryViewModel>();
            //CreateMap<IPagedList<MainCategory>, IPagedList<MainCategoryViewModel>>().ConvertUsing<PagedListConverter<MainCategory, MainCategoryViewModel>>();
            //CreateMap<IPagedList<MainCategoryViewModel>, IPagedList<MainCategory>>().ConvertUsing<PagedListConverter<MainCategoryViewModel, MainCategory>>();

            //CreateMap<NewsViewModel, News>();
            //CreateMap<News, NewsViewModel>();
            //CreateMap<IPagedList<News>, IPagedList<NewsViewModel>>().ConvertUsing<PagedListConverter<News, NewsViewModel>>();
            //CreateMap<IPagedList<NewsViewModel>, IPagedList<News>>().ConvertUsing<PagedListConverter<NewsViewModel, News>>();

            CreateMap<PaymentPackageViewModel, PaymentPackage>();
            CreateMap<PaymentPackage, PaymentPackageViewModel>();
            CreateMap<IPagedList<PaymentPackage>, IPagedList<PaymentPackageViewModel>>().ConvertUsing<PagedListConverter<PaymentPackage, PaymentPackageViewModel>>();
            CreateMap<IPagedList<PaymentPackageViewModel>, IPagedList<PaymentPackage>>().ConvertUsing<PagedListConverter<PaymentPackageViewModel, PaymentPackage>>();

            CreateMap<PaymentPackageFeatureViewModel, PaymentPackageFeature>();
            CreateMap<PaymentPackageFeature, PaymentPackageFeatureViewModel>();
            CreateMap<IPagedList<PaymentPackageFeature>, IPagedList<PaymentPackageFeatureViewModel>>().ConvertUsing<PagedListConverter<PaymentPackageFeature, PaymentPackageFeatureViewModel>>();
            CreateMap<IPagedList<PaymentPackageFeatureViewModel>, IPagedList<PaymentPackageFeature>>().ConvertUsing<PagedListConverter<PaymentPackageFeatureViewModel, PaymentPackageFeature>>();

            CreateMap<PropertyViewModel, Property>();
            CreateMap<Property, PropertyViewModel>();
            CreateMap<IPagedList<Property>, IPagedList<PropertyViewModel>>().ConvertUsing<PagedListConverter<Property, PropertyViewModel>>();
            CreateMap<IPagedList<PropertyViewModel>, IPagedList<Property>>().ConvertUsing<PagedListConverter<PropertyViewModel, Property>>();

            //CreateMap<SearchTermViewModel, SearchTerm>();
            //CreateMap<SearchTerm, SearchTermViewModel>();
            //CreateMap<IPagedList<SearchTerm>, IPagedList<SearchTermViewModel>>().ConvertUsing<PagedListConverter<SearchTerm, SearchTermViewModel>>();
            //CreateMap<IPagedList<SearchTermViewModel>, IPagedList<SearchTerm>>().ConvertUsing<PagedListConverter<SearchTermViewModel, SearchTerm>>();

            //CreateMap<ServiceViewModel, Models.Service>();
            //CreateMap<Models.Service, ServiceViewModel>();
            //CreateMap<IPagedList<Models.Service>, IPagedList<ServiceViewModel>>().ConvertUsing<PagedListConverter<Models.Service, ServiceViewModel>>();
            //CreateMap<IPagedList<ServiceViewModel>, IPagedList<Models.Service>>().ConvertUsing<PagedListConverter<ServiceViewModel, Models.Service>>();

            //CreateMap<ServicePropertyViewModel, ServiceProperty>();
            //CreateMap<ServiceProperty, ServicePropertyViewModel>();
            //CreateMap<IPagedList<ServiceProperty>, IPagedList<ServicePropertyViewModel>>().ConvertUsing<PagedListConverter<ServiceProperty, ServicePropertyViewModel>>();
            //CreateMap<IPagedList<ServicePropertyViewModel>, IPagedList<ServiceProperty>>().ConvertUsing<PagedListConverter<ServicePropertyViewModel, ServiceProperty>>();

            //CreateMap<ServiceSupplierViewModel, ServiceSupplier>();
            //CreateMap<ServiceSupplier, ServiceSupplierViewModel>();
            //CreateMap<IPagedList<ServiceSupplier>, IPagedList<ServiceSupplierViewModel>>().ConvertUsing<PagedListConverter<ServiceSupplier, ServiceSupplierViewModel>>();
            //CreateMap<IPagedList<ServiceSupplierViewModel>, IPagedList<ServiceSupplier>>().ConvertUsing<PagedListConverter<ServiceSupplierViewModel, ServiceSupplier>>();

            //CreateMap<SubCategoryViewModel, SubCategory>();
            //CreateMap<SubCategory, SubCategoryViewModel>();
            //CreateMap<IPagedList<SubCategory>, IPagedList<SubCategoryViewModel>>().ConvertUsing<PagedListConverter<SubCategory, SubCategoryViewModel>>();
            //CreateMap<IPagedList<SubCategoryViewModel>, IPagedList<SubCategory>>().ConvertUsing<PagedListConverter<SubCategoryViewModel, SubCategory>>();

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

            //CreateMap<SupplierSubCategoryPropertyViewModel, SupplierSubCategoryProperty>();
            //CreateMap<SupplierSubCategoryProperty, SupplierSubCategoryPropertyViewModel>();
            //CreateMap<IPagedList<SupplierSubCategoryProperty>, IPagedList<SupplierSubCategoryPropertyViewModel>>().ConvertUsing<PagedListConverter<SupplierSubCategoryProperty, SupplierSubCategoryPropertyViewModel>>();
            //CreateMap<IPagedList<SupplierSubCategoryPropertyViewModel>, IPagedList<SupplierSubCategoryProperty>>().ConvertUsing<PagedListConverter<SupplierSubCategoryPropertyViewModel, SupplierSubCategoryProperty>>();

            CreateMap<SupplierTypeViewModel, SupplierType>();
            CreateMap<SupplierType, SupplierTypeViewModel>();
            CreateMap<IPagedList<SupplierType>, IPagedList<SupplierTypeViewModel>>().ConvertUsing<PagedListConverter<SupplierType, SupplierTypeViewModel>>();
            CreateMap<IPagedList<SupplierTypeViewModel>, IPagedList<SupplierType>>().ConvertUsing<PagedListConverter<SupplierTypeViewModel, SupplierType>>();

            //CreateMap<UserSpecializationViewModel, UserSpecialization>();
            //CreateMap<UserSpecialization, UserSpecializationViewModel>();
            //CreateMap<IPagedList<UserSpecialization>, IPagedList<UserSpecializationViewModel>>().ConvertUsing<PagedListConverter<UserSpecialization, UserSpecializationViewModel>>();
            //CreateMap<IPagedList<UserSpecializationViewModel>, IPagedList<UserSpecialization>>().ConvertUsing<PagedListConverter<UserSpecializationViewModel, UserSpecialization>>();

            
            CreateMissingTypeMaps = true;
        }
    }
}
