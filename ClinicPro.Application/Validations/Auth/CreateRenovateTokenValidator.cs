using ClinicPro.Application.Dtos.Auth;
using FluentValidation;

namespace ClinicPro.Application.Validations.Auth
{
    public class CreateRenovateTokenValidator : AbstractValidator<RenovateTokenRequest>
    {

        public CreateRenovateTokenValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("El token es requerido.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("El rol es requerido.");
            RuleFor(x => x.IsActive)
                .NotEmpty().WithMessage("El estado de la cuenta es requerido.");

        }




    }
}
