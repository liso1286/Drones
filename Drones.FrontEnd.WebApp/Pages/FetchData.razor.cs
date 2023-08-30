using AutoMapper;
using Drones.FrontEnd.WebApp.DataServices;
using Drones.FrontEnd.WebApp.Dtos;
using Drones.FrontEnd.WebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace Drones.FrontEnd.WebApp.Pages
{
    public partial class FetchData
    {
        [Inject] private IDroneDataService _droneDataService { get; set; }
        [Inject] private IMapper _mapper { get; set; }

        //private List<DroneDto>? dronesDto;
        //private DroneMv DroneMv { get; set; } = new();
        //private bool loading = false;

        //protected override async Task OnInitializedAsync()
        //{
            //droneMv = new DroneMv();
            //dronesDto = await _droneDataService.GetAvailableDronesAsync();
        //}

        //private async Task CreateDrone()
        //{
        //    loading= true;
        //    var droneDto = _mapper.Map<DroneDto>(DroneMv);
        //    var response = await _droneDataService.CreateModifyDroneAsync(droneDto);
        //    if (response == HttpStatusCode.OK)
        //        dronesDto = await _droneDataService.GetAvailableDronesAsync();
        //    StateHasChanged();
        //    loading= false;
        //}
    }
}
