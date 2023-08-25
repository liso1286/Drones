using Drones.Dtos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Drones.Controllers.Queries
{
    [Route("/[controller]")]
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

        [HttpPost("CheckMedicationByDrone")]
        [ProducesResponseType(typeof(IEnumerable<MedicationResponse>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CheckMedicationByDrone([FromBody] CheckMedicationByDroneRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpPost("CheckAvailablesDronesForLoading")]
        [ProducesResponseType(typeof(IEnumerable<DroneResponse>), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CheckAvailablesDronesForLoading([FromBody] CheckAvailablesDronesForLoadingRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpPost("CheckBatteryLevelForDrone")]
        [ProducesResponseType(typeof(CheckBatteryLevelForDroneResponse), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> CheckBatteryLevelForDrone([FromBody] CheckBatteryLevelForDroneRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }
    }
}
