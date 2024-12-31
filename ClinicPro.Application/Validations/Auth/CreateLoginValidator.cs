using ClinicPro.Application.Dtos.Auth;
using FluentValidation;

namespace ClinicPro.Application.Validations.Auth
{
    public class CreateLoginValidator : AbstractValidator<LoginRequest>
    {

        public CreateLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(4).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
