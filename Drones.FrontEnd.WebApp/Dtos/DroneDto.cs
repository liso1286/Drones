using Newtonsoft.Json;

namespace Drones.FrontEnd.WebApp.Dtos
{
    public partial class DroneDto
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }
        [JsonProperty("model")]
        public ModelType Model { get; set; }
        [JsonProperty("weightLimit")]
        public decimal WeightLimit { get; set; }
        [JsonProperty("batteryCapacity")]
        public decimal BatteryCapacity { get; set; }
        [JsonProperty("state")]
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
