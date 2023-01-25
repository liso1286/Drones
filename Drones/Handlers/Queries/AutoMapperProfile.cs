using AutoMapper;
using Drones.Dtos.Queries;
using Drones.Entities;

namespace Drones.Handlers.Queries
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Drone, DroneResponse>();
            CreateMap<Drone, CheckBatteryLevelForDroneResponse>();
            CreateMap<Medication, MedicationResponse>();
        }
    }
}
