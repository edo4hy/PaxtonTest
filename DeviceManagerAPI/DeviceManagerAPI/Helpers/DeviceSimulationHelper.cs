public class DeviceSimulationHelper
{
    private readonly AppSettings _settings;

    public DeviceSimulationHelper(AppSettings settings)
    {
        _settings = settings;
    }

   private static readonly Random _random = new();

    public static double CalculateSignalChange(double currentSignal, AppSettings settings)
    {
        double percentChange = _random.NextDouble() * settings.PercentageChangeFactor;
        bool increase = _random.Next(2) == 0;
        int delta = (int)(currentSignal * percentChange);

        double newSignal = increase ? currentSignal + delta : currentSignal - delta;
        return Math.Clamp(Math.Round(newSignal, 1), 0.1, 99.9);
    }

    public static DeviceStatus CalculateOnlineStatus(DeviceStatus currentStatus, AppSettings settings)
    {
        if (_random.NextDouble() < settings.OfflineChangeFactor)
        {
            return currentStatus == DeviceStatus.Online ? DeviceStatus.Offline : DeviceStatus.Online;
        }
        return currentStatus;
    }
}
