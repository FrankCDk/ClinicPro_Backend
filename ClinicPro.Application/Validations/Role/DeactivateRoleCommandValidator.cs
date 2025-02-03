using ClinicPro.Application.Features.Roles.Commands.Deactivate;
using FluentValidation;

namespace ClinicPro.Application.Validations.Role
{
    public class DeactivateRoleCommandValidator : AbstractValidator<DeactivateRoleCommand>
    {

        public DeactivateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id es requerido.");
        }


    }
}
