using Drones.Entities;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using static Drones.Entities.Drone;

namespace Drones.Dtos.Queries
{
    public record CheckMedicationByDroneRequest(string SerialNumber) :
        IRequest<IEnumerable<MedicationResponse>>;


    public class MedicationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
    }

    public record CheckAvailablesDronesForLoadingRequest() :
        IRequest<IEnumerable<DroneResponse>>;

    public class DroneResponse
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public ModelType Model { get; set; }
        public decimal WeightLimit { get; set; }
        public decimal BatteryCapacity { get; set; }
        public StateLevel State { get; set; }
    }

    public record CheckBatteryLevelForDroneRequest(string SerialNumber) :
        IRequest<CheckBatteryLevelForDroneResponse>;

    public class CheckBatteryLevelForDroneResponse
    {
        public decimal BatteryCapacity { get; set; }
    }
}
