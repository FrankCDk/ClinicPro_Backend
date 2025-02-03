using ClinicPro.Application.Features.Roles.Commands.Create;
using FluentValidation;

namespace ClinicPro.Application.Validations.Role
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {

        public CreateRoleCommandValidator()
        {
            
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required").MaximumLength(2).WithMessage("Longitud maxima del código es de 2 caracteres.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Longitud maxima del nombre es de 50 caracteres.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive is required");

        }
    }
}
