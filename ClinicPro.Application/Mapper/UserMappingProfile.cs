using AutoMapper;
using ClinicPro.Application.Dtos.Auth;
using ClinicPro.Application.Dtos.Login;
using ClinicPro.Core.Entities;

namespace ClinicPro.Application.Mapper
{
    public class UserMappingProfile : Profile
    {

        public UserMappingProfile() 
        {
            
            CreateMap<LoginRequest, User>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserPasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<User, LoginResponse>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserLastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRol))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.UserIsActive));

            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserRol, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.UserPasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserDateBirth, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.UserIsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<RenovateTokenRequest, User>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserRol, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.UserIsActive, opt => opt.MapFrom(src => src.IsActive));

        }

    }
}
