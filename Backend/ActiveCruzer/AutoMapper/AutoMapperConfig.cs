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
using RequestType = ActiveCruzer.Models.RequestType;

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

            CreateMap<CreateRequestDto, Request>()
                .ForMember(dest => dest.RequestType,
                    opts => opts.MapFrom(src => src.RequestType))
                .ForMember(dest => dest.City,
                    opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.Description,
                    opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Email,
                    opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName,
                    opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                    opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber,
                    opts => opts.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Street,
                    opts => opts.MapFrom(src => src.Street))
                .ForMember(dest => dest.Zip,
                    opts => opts.MapFrom(src => src.Zip));

            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.Description,
                    opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Email,
                    opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName,
                    opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                    opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber,
                    opts => opts.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Street,
                    opts => opts.MapFrom(src => src.Street))
                .ForMember(dest => dest.Zip,
                    opts => opts.MapFrom(src => src.Zip))
                .ForMember(dest => dest.City,
                    opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.Status,
                    opts => opts.MapFrom(src => src.Status));
        }
    }
}