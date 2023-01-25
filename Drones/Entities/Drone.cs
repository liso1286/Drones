using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Drones.Entities
{
    public class Drone
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public string SerialNumber { get; set; }
        [NotNull]
        public string Model { get; set; }
        [NotNull]
        public decimal WeightLimit { get; set; }
        [NotNull]
        public decimal BatteryCapacity { get; set; }
        [NotNull]
        public string State { get; set; }
        public IEnumerable<Medication> Medications { get; set; }

        public Drone(
            string serialNumber,
            string model,
            decimal weightLimit,
            decimal batteryCapacity,
            string state)
        {
            SerialNumber = serialNumber;
            Model = model;
            WeightLimit = weightLimit;
            BatteryCapacity = batteryCapacity;
            State = state;
            Medications = new List<Medication>();
        }
    }
}
