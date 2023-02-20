using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Drones.FrontEnd.WebApp.ViewModels
{
    public class DroneMv
    {
        public int Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public ModelType Model { get; set; }
        [Required]
        public decimal WeightLimit { get; set; }
        [Required]
        public decimal BatteryCapacity { get; set; }
        [Required]
        public StateLevel State { get; set; }

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
