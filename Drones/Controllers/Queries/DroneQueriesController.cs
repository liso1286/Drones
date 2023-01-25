using Drones.Dtos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Drones.Controllers.Queries
{
    [Route("api/[controller]")]
    // [Authorize(Roles ="ADMIN,USUARIO")]
    [AllowAnonymous]
    [ApiController]
    public class DroneQueriesController : Controller
    {
        private readonly IMediator _mediator;

        public DroneQueriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("CheckMedicationByDrone")]
        [ProducesResponseType(typeof(IEnumerable<MedicationResponse>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CheckMedicationByDrone([FromQuery] CheckMedicationByDroneRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpGet("CheckAvailablesDronesForLoading")]
        [ProducesResponseType(typeof(IEnumerable<DroneResponse>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CheckAvailablesDronesForLoading([FromQuery] CheckAvailablesDronesForLoadingRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpGet("CheckBatteryLevelForDrone")]
        [ProducesResponseType(typeof(CheckBatteryLevelForDroneResponse), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CheckBatteryLevelForDrone([FromQuery] CheckBatteryLevelForDroneRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }
    }
}
