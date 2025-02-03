using ClinicPro.Application.Validations.Role;
using ClinicPro.Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Deactivate
{
    public class DeactivateRoleCommandHandler : IRequestHandler<DeactivateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        public DeactivateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeactivateRoleCommand request, CancellationToken cancellationToken)
        {

            ValidationResult validationResult = new DeactivateRoleCommandValidator().Validate(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _roleRepository.DeactivateRole(request.Id);

        }
    }
}
