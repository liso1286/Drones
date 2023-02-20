using AutoMapper;
using Drones.FrontEnd.WebApp.Dtos;
using Drones.FrontEnd.WebApp.ViewModels;

namespace Drones.FrontEnd.WebApp.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DroneMv, DroneDto>().ReverseMap();
        }
    }
}
