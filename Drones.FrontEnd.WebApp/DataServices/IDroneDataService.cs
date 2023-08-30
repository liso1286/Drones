using Drones.FrontEnd.WebApp.Dtos;
using System.Net;

namespace Drones.FrontEnd.WebApp.DataServices
{
    public interface IDroneDataService
    {
        Task<List<DroneDto>> GetAvailableDronesAsync();
        Task<HttpStatusCode> CreateModifyDroneAsync(DroneDto droneDto);
    }
}
