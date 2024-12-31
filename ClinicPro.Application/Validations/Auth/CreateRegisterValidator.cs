using ClinicPro.Application.Dtos.Auth;
using FluentValidation;

namespace ClinicPro.Application.Validations.Auth
{
    public class CreateRegisterValidator : AbstractValidator<RegisterRequest>
    {
        public CreateRegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MinimumLength(3).WithMessage("El apellido debe tener al menos 3 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(4).WithMessage("La contraseña debe tener al menos 4 caracteres.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("La confirmación de la contraseña es requerida.")
                .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden.");
            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.");
        }

    }
}
