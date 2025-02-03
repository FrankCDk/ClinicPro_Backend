using ClinicPro.Application.Dtos.Doctor;
using ClinicPro.Application.Features.Doctors.Commands.CreateDoctor;
using ClinicPro.Application.Features.Doctors.Commands.UpdateDoctor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPro.Presentation.Controllers
{


    [Route("api/v1/doctor/[action]")]
    [ApiController]
    //[Authorize]
    public class DoctorController : Controller
    {

        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorCommand request)
        {
            var doctor = await _mediator.Send(request);
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorCommand request)
        {
            var doctor = await _mediator.Send(request);
            return Ok();
        }


    }
}
