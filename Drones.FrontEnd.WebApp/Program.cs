using BlazorStrap;
using Drones.FrontEnd.WebApp;
using Drones.FrontEnd.WebApp.DataServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

Console.WriteLine("Blazor APICLIENT has started");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddBlazorStrap();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["api_base_url"]) });
builder.Services.AddHttpClient<IDroneDataService, RESTDroneDataService>
        (x => x.BaseAddress = new Uri(builder.Configuration["api_base_url"]));
await builder.Build().RunAsync();
