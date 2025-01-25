using ClinicPro.Application.Dtos.Role;
using MediatR;

namespace ClinicPro.Application.Features.Roles.Queries
{
    public class ReadRolesQuery : IRequest<List<RoleResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
