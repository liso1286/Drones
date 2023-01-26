using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Drones.Entities
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public decimal Weight { get; set; }
        [NotNull]
        public string Code { get; set; }
        [NotNull]
        public byte[] Image { get; set; }

        [ForeignKey("Drone")]
        public int DroneId { get; set; }
        public virtual Drone Drone { get; set; }

        private Medication()
        {
                
        }
      
        public Medication(string code, string name, decimal weight)
        {
            Code = code;
            Name = name;
            Weight = weight;
            Image = Array.Empty<byte>();
        }
    }
}
