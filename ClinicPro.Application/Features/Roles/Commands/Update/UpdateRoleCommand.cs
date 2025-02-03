using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommand : IRequest<bool>
    {

        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
