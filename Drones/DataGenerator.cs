using Drones.Entities;
using Microsoft.EntityFrameworkCore;
using static Drones.Entities.Drone;

namespace Drones
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApiDbContext>>()))
            {
                if (context.Drones.Any())
                {
                    return; 
                }

                context.Drones.AddRange(
                    new Drone("D1", ModelType.Lightweight, 400.00M)
                        .CreateAddMedications("M1", "Aspirin", 200.00M, imagePath: string.Empty)
                        .CreateAddMedications("M2", "Ibuprofen", 100.00M, imagePath: string.Empty)
                        .CreateAddMedications("M3", "Vitamin-C", 100.00M, imagePath: string.Empty),
                    new Drone("D2", ModelType.Heavyweight, 300.00M)
                        .CreateAddMedications("M4", "Acetaminophen", 50.00M, imagePath: string.Empty)
                        .CreateAddMedications("M5", "Albendazole", 100.00M, imagePath: string.Empty)
                        .CreateAddMedications("M6", "Amoxicillin", 100.00M, imagePath: string.Empty),
                    new Drone("D3", ModelType.Cruiserweight, 500.00M)
                        .CreateAddMedications("M7", "Ampicillin", 10.00M, imagePath: string.Empty)
                        .CreateAddMedications("M8", "Atenolol", 50.00M, imagePath: string.Empty)
                        .CreateAddMedications("M9", "Diazepam", 20.00M, imagePath: string.Empty)
                        .CreateAddMedications("M10", "Fluconazole", 20.00M, imagePath: string.Empty)
                        .CreateAddMedications("M11", "Fluorouracil", 10.00M, imagePath: string.Empty),
                    new Drone("D4", ModelType.Lightweight, 200.00M)
                        .CreateAddMedications("M12", "Ketamine", 30.00M, imagePath: string.Empty)
                        .CreateAddMedications("M13", "Lidocaine", 30.00M, imagePath: string.Empty)
                        .CreateAddMedications("M14", "Meloxicam", 30.00M, imagePath: string.Empty)
                        .CreateAddMedications("M15", "Metformin", 10.00M, imagePath: string.Empty),
                    new Drone("D5", ModelType.Heavyweight, 500.00M),
                    new Drone("D6", ModelType.Heavyweight, 250.00M));

                context.SaveChanges();
            }
        }
    }
}
