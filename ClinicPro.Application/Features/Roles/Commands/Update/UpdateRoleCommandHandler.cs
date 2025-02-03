using AutoMapper;
using ClinicPro.Application.Validations.Role;
using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            ValidationResult result = new UpdateRoleCommandValidator().Validate(request);

            if(result.IsValid == false)
            {
                throw new ValidationException(result.Errors);
            }

            //Mapeo
            Role role = _mapper.Map<Role>(request);

            //Validamos si existe
            _ = await _roleRepository.GetRoleById(role.RolId) ?? throw new Exception("El rol no existe.");

            //Actualizamos
            return await _roleRepository.UpdateRole(role);

        }
    }
}
