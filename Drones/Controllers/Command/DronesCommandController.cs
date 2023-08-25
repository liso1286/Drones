using Drones.Dtos.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drones.Controllers.Command
{
    [Route("/[controller]")]
    //[Authorize(Roles = "ADMIN")]
    [ApiController]
    public class DronesCommandController : Controller
    {
        private readonly IMediator _mediator;

        public DronesCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateModifyDrone")]
        [ProducesResponseType(typeof(CreateModifyDroneResponse), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CreateModifyDrone([FromBody] CreateModifyDroneRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpPost("LoadDrone")]
        [ProducesResponseType(typeof(LoadDroneResponse), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> LoadDrone([FromBody] LoadDroneRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }
    }
}
