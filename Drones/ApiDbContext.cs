using Drones.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drones
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medication> Medications { get; set; }
    }
}
