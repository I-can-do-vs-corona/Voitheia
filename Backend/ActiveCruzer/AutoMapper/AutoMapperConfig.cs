using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.BLL;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using ActiveCruzer.Models.DTO.Request;
using ActiveCruzer.Models.Geo;
using AutoMapper;
using BingMapsRESTToolkit;
using RequestType = ActiveCruzer.Models.RequestType;

namespace ActiveCruzer.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateRequestDto, GeoQuery>()
                .ForMember(dest => dest.City,
                    opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.Street,
                    opts => opts.MapFrom(src => src.Street))
                .ForMember(dest => dest.Zip,
                    opts => opts.MapFrom(src => src.Zip));

            CreateMap<CreateRequestDto, Request>()
                .ForMember(dest => dest.RequestType,
                    opts => opts.MapFrom(src => src.Type))
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
                .ForMember(dest => dest.Id,
                    opts => opts.MapFrom(src => src.Id))
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
                    opts => opts.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type,
                    opts => opts.MapFrom(src => src.RequestType));

            CreateMap<Location, ValidatedAddress>()
                .ForMember(dest => dest.ConfidenceLevel,
                    opts => opts.MapFrom(src => src.ConfidenceLevelType))
                .ForMember(dest => dest.City,
                    opts => opts.MapFrom(src => src.Address.Locality))
                .ForMember(dest => dest.Zip,
                    opts => opts.MapFrom(src => src.Address.PostalCode))
                .ForPath(dest => dest.Coordinates.Longitude,
                    opts => opts.MapFrom(src => src.Point.GetCoordinate().Longitude))
                .ForPath(dest => dest.Coordinates.Latitude,
                    opts => opts.MapFrom(src => src.Point.GetCoordinate().Latitude))
                .ForMember(dest => dest.Street,
                    opts => opts.MapFrom(src => src.Address.AddressLine));

        }
    }
}