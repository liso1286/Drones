using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Drones.Entities
{
    public class Medication
    {
        private ILazyLoader _loader { get; set; }

        protected Medication(ILazyLoader loader)
        {
            _loader = loader;
        }

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

        private Drone _drone;
        [ForeignKey(nameof(DroneId))]
        public Drone Drone
        {
            get
            {
                _loader.Load(this, ref _drone);
                return _drone;
            }
            set { _drone = value; }
        }
    }
}
