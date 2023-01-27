using MediatR;
using System.Drawing;
using static Drones.Entities.Drone;

namespace Drones.Dtos.Command
{
    public record CreateModifyDroneRequest(
            string SerialNumber,
            int Model,
            decimal WeightLimit,
            decimal BatteryCapacity,
            StateLevel State) : IRequest<CreateModifyDroneResponse>;

    public class CreateModifyDroneResponse
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public ModelType Model { get; set; }
        public decimal WeightLimit { get; set; }
        public decimal BatteryCapacity { get; set; }
        public StateLevel State { get; set; }
    }

    public record LoadDroneRequest(
         string DroneSerialNumber,
         string Name,
         decimal Weight,
         string Code,
         string ImagePath) : IRequest<LoadDroneResponse>;

    public class LoadDroneResponse
    {
        public string DroneSerialNumber { get; set; }
        public IEnumerable<MedicationItem> Medications { get; set; }
    }

    public class MedicationItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public byte[] Image { get; set; }
    }
}
