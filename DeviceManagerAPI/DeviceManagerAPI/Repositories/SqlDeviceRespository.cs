
using Microsoft.EntityFrameworkCore;

public class SqlDeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public SqlDeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Device>> GetAllAsync() => await _context.Devices.ToListAsync();

    public async Task<Device?> GetByIdAsync(int id) => await _context.Devices.FindAsync(id);

    public async Task AddAsync(Device device)
    {
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
    }
}
