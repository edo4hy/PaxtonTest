using Microsoft.EntityFrameworkCore;

public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddDeviceRepository(this IServiceCollection services, IConfiguration config)
    {
        var mode = config["RepositoryMode"];

        if (mode == "InMemory")
        {
            services.AddSingleton<IDeviceRepository, InMemoryDeviceRepository>();
            services.AddHostedService<DeviceSimulatorService>();
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDeviceRepository, SqlDeviceRepository>();

            // Register a post-build hook to run migrations
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }
            }
        }

        return services;
    }
}
