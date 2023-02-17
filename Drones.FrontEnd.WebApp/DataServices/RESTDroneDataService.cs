using Drones.FrontEnd.WebApp.Dtos;
using System.Net.Http.Json;

namespace Drones.FrontEnd.WebApp.DataServices
{
    public class RESTDroneDataService : IDroneDataService
    {
        private readonly HttpClient _httpClient;

        public RESTDroneDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DroneDto>> GetAvailableDronesAsync()
        {
            var drone = new DroneDto();
            var response =
            await
            _httpClient
            .PostAsJsonAsync("DroneQueries/CheckAvailablesDronesForLoading", drone);

            return
                await response.Content.ReadFromJsonAsync<List<DroneDto>>() ?? new List<DroneDto>();

        }
    }
}
