using AutoMapper;
using ClinicPro.Application.Dtos.Doctor;
using ClinicPro.Core.Entities;

namespace ClinicPro.Application.Mapper
{
    internal class DoctorMappingProfile : Profile
    {

        public DoctorMappingProfile()
        {
            
            //CreateMap<DoctorRequest, Doctor>()
            //    .ForMember(dest => dest.DoctorFirstName, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.DoctorLastName, opt => opt.MapFrom(src => src.LastName))
            //    .ForMember(dest => dest.DoctorSpecialty, opt => opt.MapFrom(src => src.Specialty));

        }


    }
}
