using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using AutoMapper;
using GeoCoordinatePortable;

namespace ActiveCruzer.AutoMapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.Street,
                    opts => opts.MapFrom(src => src.Street))
                .ForMember(dest => dest.City,
                    opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.FirstName,
                    opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                    opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Zip,
                    opts => opts.MapFrom(src => src.Zip))
                .ForMember(dest => dest.Email,
                    opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail,
                    opts => opts.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.UserName,
                    opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName,
                    opts => opts.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.Latitude,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Longitude,
                    opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed,
                    opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp,
                    opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed,
                    opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled,
                    opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled,
                    opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp,
                    opts => opts.Ignore())
                .ForMember(dest => dest.AccessFailedCount,
                    opt => opt.Ignore());


        }
    }
}