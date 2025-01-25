using ClinicPro.Application.Dtos.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Application.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQuery : IRequest<List<DoctorResponseList>>
    {

    }
}
