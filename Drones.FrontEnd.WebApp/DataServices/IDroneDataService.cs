using Drones.FrontEnd.WebApp.Dtos;

namespace Drones.FrontEnd.WebApp.DataServices
{
    public interface IDroneDataService
    {
        Task<List<DroneDto>> GetAvailableDronesAsync();
    }
}
