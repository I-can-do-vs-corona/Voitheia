using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.DTO.Geo;
using ActiveCruzer.Models.DTO.Request;
using ActiveCruzer.Models.Geo;
using AutoMapper;

namespace ActiveCruzer.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<GetGeoCodeQueryParameters, GeoQuery>()
                .ForMember(dest => dest.City,
                    opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.Country,
                    opts => opts.MapFrom(src => src.Country))
                .ForMember(dest => dest.Street,
                    opts => opts.MapFrom(src => src.Street))
                .ForMember(dest => dest.Zip,
                    opts => opts.MapFrom(src => src.Zip));

            CreateMap<Coordinates, CoordinatesDto>()
                .ForMember(dest => dest.Latitude,
                    opts => opts.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude,
                    opts => opts.MapFrom(src => src.Longitude));

            CreateMap<CreateRequestDto, Request>()
                .ForMember(dest => dest.Coordinates,
                    opts => opts.MapFrom(src => src.Coordinates));
            CreateMap<Request, CreateRequestDto>();
            CreateMap<GetRequestResponse, Request>();
            CreateMap<Request, GetRequestResponse>();
            CreateMap<RequestDto, Request>();
            CreateMap<Request, RequestDto>();
        }
    }
}