using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceRepository _repository;

    public DevicesController(IDeviceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var devices = await _repository.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var device = await _repository.GetByIdAsync(id);
        return device is not null ? Ok(device) : NotFound($"Device with ID {id} not found.");
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Device device)
    {
        await _repository.AddAsync(device);
        return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
    }
}
