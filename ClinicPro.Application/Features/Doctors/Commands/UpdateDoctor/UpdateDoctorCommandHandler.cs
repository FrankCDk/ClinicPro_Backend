using ClinicPro.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, int>
    {

        private readonly IDoctorRepository _doctorRepository;
        public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<int> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            throw new Exception("Metodo actualizacion no implementado");
        }
    }
}
