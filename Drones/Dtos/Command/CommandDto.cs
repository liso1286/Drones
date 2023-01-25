using MediatR;

namespace Drones.Dtos.Command
{
    public record CreateModifyDroneRequest(
            int Id,
            string SerialNumber,
            int Model,
            decimal WeightLimit,
            decimal BatteryCapacity,
            int State) : IRequest<CreateModifyDroneResponse>;

    public class CreateModifyDroneResponse
    {
        public int Id { get; set; }
        public string SerialNumber { get; }
        public int Model { get; }
        public decimal WeightLimit { get; }
        public decimal BatteryCapacity { get; }
        public int State { get; }
    }
}
