using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Drones.Entities
{
    public class Drone
    {
        private ILazyLoader _loader { get; set; }

        protected Drone(ILazyLoader loader)
        {
            _loader = loader;
        }

        private Drone()
        {

        }

        public Drone(string serialNumber, ModelType model, decimal weightLimit)
        {
            SerialNumber = serialNumber;
            Model = model;
            WeightLimit = weightLimit;
            BatteryCapacity = 100.00M;
            State = StateLevel.Idle;
        }

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

        private HashSet<Medication> _medications;
        [InverseProperty("Drone")]
        public IEnumerable<Medication> Medications
        {
            get
            {
                _loader.Load(this, ref _medications);
                return _medications;
            }
        }

        public Drone CreateAddMedications(
            string code,
            string name,
            decimal weight,
            string imagePath)
        {

            var medication = new Medication(code, name, weight, image: null);
            if (Medications is null)
                _medications = new HashSet<Medication>();
            _medications.Add(medication);
            return this;
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
