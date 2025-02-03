using AutoMapper;
using ClinicPro.Application.Dtos.Role;
using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Application.Features.Roles.Queries
{
    public class ReadRolesQueryHandler : IRequestHandler<ReadRolesQuery, List<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public ReadRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleResponse>> Handle(ReadRolesQuery request, CancellationToken cancellationToken)
        {

            List<Role> lista = await _roleRepository.GetAllRoles(_mapper.Map<Role>(request));

            return _mapper.Map<List<RoleResponse>>(lista);


        }
    }
}
