using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .ForMember(dest => dest.requestDate, act => act.MapFrom(x => x.Created))
                ;


            CreateMap<ContentManagement, NewsItem>();

            CreateMissingTypeMaps = true;

        }
    }
}
