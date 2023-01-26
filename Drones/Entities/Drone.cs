using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public ModelType Model { get; set; }
        [NotNull]
        public decimal WeightLimit { get; set; }
        [NotNull]
        public decimal BatteryCapacity { get; set; }
        [NotNull]
        public StateLevel State { get; set; }
        [InverseProperty("Drone")]
        public virtual ICollection<Medication> Medications { get; set; }

        public Drone()
        {
            BatteryCapacity = 100.00M;
            State = StateLevel.Idle;
            Medications = new List<Medication>();
        }

        public enum StateLevel
        {
            Idle = 1,
            Loading = 2,
            Loaded = 3,
            Delivering = 4,
            Delivered = 5,
            Returning = 6
        }

        public enum ModelType
        {
            Lightweight = 1,
            Middleweight = 2,
            Cruiserweight = 3,
            Heavyweight = 4
        }
    }
}
