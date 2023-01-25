using Drones;
using Drones.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder
            .Services
            .AddDbContext<ApiDbContext>
                (options => options.UseSqlServer(
                                        builder.Configuration.GetConnectionString("ApiConnection")));

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
            var jobKey = new JobKey("SendEmailJob");
            q.AddJob<CreateBatteryLevelLogJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("SendEmailJob-trigger")
                //This Cron interval can be described as "run every minute" (when second is zero)
                .WithCronSchedule("0 * * ? * *")
            );
        });
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        var app = builder.Build();

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