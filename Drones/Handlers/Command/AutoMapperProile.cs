using AutoMapper;
using Drones.Dtos.Command;
using Drones.Entities;

namespace Drones.Handlers.Command
{
    public class AutoMapperProile : Profile
    {
        public AutoMapperProile()
        {
            CreateMap<CreateModifyDroneRequest, Drone>();
            CreateMap<Drone, CreateModifyDroneResponse>();
            CreateMap<Medication, MedicationItem>();
        }
    }
}
