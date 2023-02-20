using Drones.FrontEnd.WebApp.Dtos;
using System.Net;
using static Drones.FrontEnd.WebApp.Dtos.DroneDto;

namespace Drones.FrontEnd.WebApp.DataServices
{
    public interface IDroneDataService
    {
        Task<List<DroneDto>> GetAvailableDronesAsync();
        Task<HttpStatusCode> CreateModifyDroneAsync(DroneDto droneDto);
    }
}
