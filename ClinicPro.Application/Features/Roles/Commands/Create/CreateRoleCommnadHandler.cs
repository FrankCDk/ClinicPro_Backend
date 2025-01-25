using AutoMapper;
using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using MediatR;

namespace ClinicPro.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommnadHandler : IRequestHandler<CreateRoleCommand, bool>
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommnadHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            return await _roleRepository.CreateRole(_mapper.Map<Role>(request));            

        }
    }
}
