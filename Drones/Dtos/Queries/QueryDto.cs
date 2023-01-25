using Drones.Entities;
using MediatR;

namespace Drones.Dtos.Queries
{
    public record CheckMedicationByDroneRequest(string SerialNumber) :
        IRequest<CheckMedicationByDroneResponse>;

    public class CheckMedicationByDroneResponse
    {
        public IEnumerable<Medication> Medications { get; set; }
    }

    public record CheckAvailablesDronesForLoadingRequest() :
        IRequest<CheckAvailablesDronesForLoadingResponse>;

    public class CheckAvailablesDronesForLoadingResponse
    {
        public IEnumerable<Drone> AvailableDrones { get; set; }
    }

    public record CheckBatteryLevelForDroneRequest(string SerialNumber) :
        IRequest<CheckBatteryLevelForDroneResponse>;

    public class CheckBatteryLevelForDroneResponse
    {
        public decimal BatteryLevel { get; set; }
    }
}
