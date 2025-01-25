using ClinicPro.Core.Interfaces;
using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, int>
    {

        private readonly IRoleRepository _roleRepository;
        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
