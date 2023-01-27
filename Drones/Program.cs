using Drones;
using Drones.Entities;
using Drones.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static Drones.Entities.Drone;

internal class Program
{
    private static void Main(string[] args)
    {
        //Register Context for connecting to a SQLServer DB
        var builder = WebApplication.CreateBuilder(args);
        /*builder
            .Services
            .AddDbContext<ApiDbContext>
                (options => options
                            .UseSqlServer
                                (builder.Configuration
                                        .GetConnectionString("ApiConnection")));*/
        
        //Register Context to use a In-Memory DB
        builder
            .Services
            .AddDbContext<ApiDbContext>
                (opt => opt.UseInMemoryDatabase(databaseName: "DroneApiDB"));

        // Add services to the container.
        builder
            .Services
            .AddCors(options => options.AddPolicy("AllowedApp", builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,
                             ValidIssuer = builder.Configuration["JWT:Issuer"],
                             ValidAudience = builder.Configuration["JWT:Audience"],
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                         });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

        builder.Services.AddQuartz(q =>
        {
            // base Quartz scheduler, job and trigger configuration
        });

        // ASP.NET Core hosting
        builder.Services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });

        builder.Services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            // Just use the name of your job that you created in the Jobs folder.
            var jobKey = new JobKey("RegisterDroneBatteryLogJob");
            q.AddJob<CreateBatteryLevelLogJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("RegisterDroneBatteryLogJob-trigger")
                //This Cron interval can be described as "run every minute" (when second is zero)
                .WithCronSchedule("0 * * ? * *")
            );
        });
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        var app = builder.Build();

        #region Configure the preload of data to DB In Memory through DataGenerator
        // Find the service layer within our scope.
        using (var scope = app.Services.CreateScope())
        {
            // Get the instance of BoardGamesDBContext in our services layer
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApiDbContext>();

            // Call the DataGenerator to create sample data
            DataGenerator.Initialize(services);
        }

        #endregion

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("AllowedApp");
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}