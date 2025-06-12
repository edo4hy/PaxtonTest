public class InMemoryDeviceRepository : IDeviceRepository
{
    private readonly List<Device> _devices = new()
    {
        new Device { Id = 1, Name = "Camera 1", DeviceType = DeviceType.Camera, Status = DeviceStatus.Online, SignalStrength = 14 },
        new Device { Id = 2, Name = "Camera 2", DeviceType = DeviceType.Camera, Status = DeviceStatus.Online, SignalStrength = 14 },
    };

    public Task<IEnumerable<Device>> GetAllAsync()
    {
        return Task.FromResult(_devices.AsEnumerable());
    }

    public Task<Device?> GetByIdAsync(int id)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        return Task.FromResult(device);
    }

    public Task AddAsync(Device device)
    {
        device.Id = _devices.Any() ? _devices.Max(d => d.Id) + 1 : 1;
        _devices.Add(device);
        return Task.CompletedTask;
    }

    public Task UpdateDeviceAsync(Device device)
    {
        var index = _devices.FindIndex(d => d.Id == device.Id);
        if (index >= 0)
        {
            _devices[index] = device;
        }
        return Task.CompletedTask;
    }
}
