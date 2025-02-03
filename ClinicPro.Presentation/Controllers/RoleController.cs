using ClinicPro.Application.Features.Roles.Commands.Create;
using ClinicPro.Application.Features.Roles.Commands.Deactivate;
using ClinicPro.Application.Features.Roles.Commands.Update;
using ClinicPro.Application.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPro.Presentation.Controllers
{

    [ApiController]
    [Route("api/v1/role/[action]")]
    [Authorize]
    public class RoleController : Controller
    {

        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] ReadRolesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> DeactivateRole([FromQuery] DeactivateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
