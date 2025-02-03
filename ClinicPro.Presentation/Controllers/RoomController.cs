using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPro.Presentation.Controllers
{
    [Route("api/v1/room/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            mediator = _mediator;
        }




    }
}
