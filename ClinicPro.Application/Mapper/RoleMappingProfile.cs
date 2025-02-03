using AutoMapper;
using ClinicPro.Application.Dtos.Role;
using ClinicPro.Application.Features.Roles.Commands.Create;
using ClinicPro.Application.Features.Roles.Commands.Update;
using ClinicPro.Application.Features.Roles.Queries;
using ClinicPro.Core.Entities;

namespace ClinicPro.Application.Mapper
{
    internal class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<CreateRoleCommand, Role>()
                .ForMember(dest => dest.RolCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.RolDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RolName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RolIsActive, memberOptions: opt => opt.MapFrom(src => src.IsActive));

            CreateMap<UpdateRoleCommand, Role>()
                .ForMember(dest =>  dest.RolId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RolCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.RolDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RolName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RolIsActive, memberOptions: opt => opt.MapFrom(src => src.IsActive));


            CreateMap<ReadRolesQuery, Role>()
                .ForMember(dest => dest.RolCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.RolName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RolIsActive, memberOptions: opt => opt.MapFrom(src => src.IsActive));

            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RolId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.RolCode))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RolDescription))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RolName))
                .ForMember(dest => dest.IsActive, memberOptions: opt => opt.MapFrom(src => src.RolIsActive));
        }
    }
}
