using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

public class DeviceSimulatorService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<DeviceHub> _hubContext;
    private readonly AppSettings _settings;

    public DeviceSimulatorService(IServiceScopeFactory scopeFactory,
    IHubContext<DeviceHub> hubContext, IOptions<AppSettings> options )
    {
        _scopeFactory = scopeFactory;
        _hubContext = hubContext;
        _settings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IDeviceRepository>();

            var updatedDevices = await SimulateDeviceChangesAsync(repo);

            await _hubContext.Clients.All.SendAsync("DeviceUpdate", updatedDevices);

            await Task.Delay(TimeSpan.FromSeconds(_settings.LoopDelaySeconds), stoppingToken);
        }
    }

    public async Task<List<Device>> SimulateDeviceChangesAsync(IDeviceRepository repo)
    {
        if (repo is not InMemoryDeviceRepository memoryRepo) return new();

        var devices = await memoryRepo.GetAllAsync();
        if (!devices.Any()) return new();

        var now = DateTime.Now;

        var deviceList = devices.ToList();

        foreach (var device in deviceList)
        {
            device.SignalStrength = DeviceSimulationHelper.CalculateSignalChange(device.SignalStrength, _settings);
            device.Status = DeviceSimulationHelper.CalculateOnlineStatus(device.Status, _settings);
            device.UpdatedDate = now;

            await memoryRepo.UpdateDeviceAsync(device);
        }

        return deviceList;
    }
}
