using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
