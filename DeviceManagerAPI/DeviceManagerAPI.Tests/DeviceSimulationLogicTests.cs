using System.Threading.Tasks;
using Xunit;

public class DeviceSimulationHelperTests
{
    private readonly AppSettings _settings = new()
    {
        PercentageChangeFactor = 0.2,
        OfflineChangeFactor = 0.1,
        LoopDelaySeconds = 1
    };

    private DeviceSimulationHelper CreateHelper(AppSettings? settings = null)
    {
        return new DeviceSimulationHelper(settings ?? _settings);
    }

    [Theory]
    [InlineData(0.1)]
    [InlineData(1.0)]
    [InlineData(10.0)]
    [InlineData(50.0)]
    [InlineData(99.9)]
    public void CalculateSignalChange_ShouldReturnValueWithinBounds(double initialSignal)
    {
        var helper = CreateHelper();

        var newSignal = DeviceSimulationHelper.CalculateSignalChange(initialSignal, _settings);
        Assert.InRange(newSignal, 0.1, 99.9);
    }


    [Theory]
    [InlineData(DeviceStatus.Online, 0.0, DeviceStatus.Online)]  
    [InlineData(DeviceStatus.Offline, 0.0, DeviceStatus.Offline)]
    [InlineData(DeviceStatus.Online, 1.0, DeviceStatus.Offline)]  
    [InlineData(DeviceStatus.Offline, 1.0, DeviceStatus.Online)]
    public void CalculateOnlineStatus_ShouldToggleBasedOnProbability(
        DeviceStatus initialStatus, double offlineChangeFactor, DeviceStatus expectedStatus)
    {
        var settings = new AppSettings { OfflineChangeFactor = offlineChangeFactor };
        var helper = CreateHelper(settings);

        var result = DeviceSimulationHelper.CalculateOnlineStatus(initialStatus, _settings);
        Assert.Equal(expectedStatus, result);
    }
}
