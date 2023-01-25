using System.ComponentModel.DataAnnotations;
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

        public Medication(string name, decimal weight, string code)
        {
            Name = name;
            Weight = weight;
            Code = code;
            Image = Array.Empty<byte>();
        }
    }
}
