using ClinicPro.Application.Features.Roles.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Application.Validations.Role
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {

        public UpdateRoleCommandValidator()
        {
            
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required").MaximumLength(2).WithMessage("Longitud maxima del código es de 2 caracteres.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Longitud maxima del nombre es de 50 caracteres.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive is required");

        }
    }
}
