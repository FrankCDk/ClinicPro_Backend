using ClinicPro.Application.Features.Roles.Commands.Create;
using ClinicPro.Application.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPro.Presentation.Controllers
{

    [ApiController]
    [Route("api/v1/role/[action]")]
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

        //[HttpPut]
        //public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteRole([FromQuery] DeleteRoleCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}



    }
}
