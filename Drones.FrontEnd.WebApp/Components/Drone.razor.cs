using Microsoft.AspNetCore.Components;

namespace Drones.FrontEnd.WebApp.Components
{
    public partial class Drone: ComponentBase
    {
        [Parameter]
        public string Id { get; set; } = "D1";
    }
}
