using Quartz;

namespace Drones.Tasks
{
    public class CreateBatteryLevelLogJob : IJob
    {
        private readonly ApiDbContext _dbContext;
        private string _logMessage = string.Empty;

        public CreateBatteryLevelLogJob(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var batteryData =
                 _dbContext
                 .Drones
                 .ToList()
                 .Select(x =>
                 {
                     var data = $"\nDrone: {x.SerialNumber}--->Battery Level: {x.BatteryCapacity}";
                     return data;
                 })
                 .Append("\n-------------------------------")
                 .ToList();

            _logMessage = string.Join("", batteryData);
            try
            {
                var fs = new FileStream(
                    @AppDomain.CurrentDomain.BaseDirectory + "DroneBattery.log",
                    FileMode.OpenOrCreate,
                    FileAccess.Write);
                var sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                if (fs.Length == 0)
                    sw.WriteLine("     DRONE BATTERY LOGS \n-------------------------------\n");
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + _logMessage);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }

            return Task.FromResult(true);
        }
    }
}
