using ClinicPro.Application.Dtos.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Application.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, List<DoctorResponseList>>
    {

        public Task<List<DoctorResponseList>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
