using Drones.FrontEnd.WebApp.DataServices;
using Drones.FrontEnd.WebApp.Dtos;
using Microsoft.AspNetCore.Components;

namespace Drones.FrontEnd.WebApp.Pages
{
    public partial class FetchData
    {
        [Inject] private IDroneDataService DroneDataService { get; set; }

        private List<DroneDto>? drones;

        protected override async Task OnInitializedAsync()
        {
            drones = await DroneDataService.GetAvailableDronesAsync();
        }
    }
}
