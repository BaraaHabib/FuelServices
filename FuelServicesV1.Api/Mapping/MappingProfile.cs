using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Models;
using FuelServices.Api.Models.Requests;
using FuelServices.Api.Models.SupplierContacts;
using System.Linq;

namespace FuelServices.Api.Mapping
{
    public class MappingProfile : Profile

    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<AirportModel, Airport>();
            CreateMap<Airport, AirportModel>();

            CreateMap<FuelTypeModel, FuelType>()
                ;
            CreateMap<FuelType, FuelTypeModel>();

            CreateMap<OfferModel, Offer>();
            CreateMap<Offer, OfferModel>()
                .ForMember(dest => dest.Airports, opt => opt.MapFrom(q => q.AirportOffers.Select(x => x.Airport).ToList()))
                .ForMember(dest => dest.FuelTypes, opt => opt.MapFrom(q => q.OfferFuelType.Select(x => x.FuelType).ToList()))
                .ForMember(dest => dest.FuelSupplier, opt => opt.MapFrom(q => q.FuelSupplier.Name))
                //.ForMember(dest => dest.ai, opt => opt.MapFrom(q => q.AirportOffers.FirstOrDefault(f => f.OfferId == q.Id).Price))
                //.ForMember(dest => dest.Price, opt => opt.MapFrom(q => q.AirportOffers.FirstOrDefault(f => f.OfferId == q.Id).PriceUnit))
                ;

            #region Requests

            CreateMap<RequestOffers, RequestOfferModel>()
                .ForMember(dest => dest.Status, x => x.MapFrom(src => src.RStatus))
                .ForMember(dest => dest.StatusText, x => x.MapFrom(src => src.RStatus.ToString()))
                .ForMember(dest => dest.RequestOfferId, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Supplier, x => x.MapFrom(src => src.Offer.FuelSupplier.Name))
                ;
            CreateMap<RequestModel, Request>()
                .ForMember(dest => dest.Customer, act => act.Ignore())
                .ForMember(dest => dest.Airport, act => act.Ignore())
                ;

            CreateMap<Request, RequestModel>()
                .ForMember(dest => dest.Customer, act => act.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.Airport, act => act.MapFrom(src => $"{src.Airport.Name}"))
                .ForMember(dest => dest.Status, act => act.MapFrom(x => x.RequestOffers.FirstOrDefault().RStatus.ToString()))
                .ForMember(dest => dest.Price, act => act.MapFrom(x => (x.Quantity * x.RequestOffers.FirstOrDefault().AirportOffer.Price).ToString()))
                .ForMember(dest => dest.PriceUnit, act => act.MapFrom(x => x.RequestOffers.FirstOrDefault().AirportOffer.PriceUnit))
                .ForMember(dest => dest.RequestDate, act => act.MapFrom(x => x.Created))
                //.ForMember(dest => dest.RequestOffers, act => act.MapFrom(x => x.RequestOffers.Select(ro => ro.Offer.FuelSupplier.Name)))
                ;
            #endregion


            CreateMap<ContentManagement, NewsItem>();


            #region Supplier Contacts

            CreateMap<SupplierContact, GlobalContact>()
                .ForMember(dist => dist.Type, x => x.MapFrom(src => src.Contact.DisplayName));

            CreateMap<SupplierContactPerson, PersonContact>();

            #endregion
            //CreateMissingTypeMaps = true;
        }
    }
}