using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Deactivate
{
    public class DeactivateRoleCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
