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
                .ForMember(dest => dest.Usr_email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Usr_password_hash, opt => opt.MapFrom(src => src.Password));

            CreateMap<User, LoginResponse>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Usr_email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Usr_first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Usr_last_name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Usr_rol))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Usr_is_active));

            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Usr_email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Usr_first_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Usr_last_name, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Usr_rol, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Usr_password_hash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Usr_date_of_birth, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Usr_is_active, opt => opt.MapFrom(src => src.IsActive));
        }

    }
}
