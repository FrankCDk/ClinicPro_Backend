using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommand : IRequest<bool>
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
