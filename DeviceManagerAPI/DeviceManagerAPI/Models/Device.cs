using System.Text.Json.Serialization;

public class Device
{
    public int Id { get; set; }
    public required string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DeviceType DeviceType { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DeviceStatus Status { get; set; }
    public double SignalStrength { get; set; }

    public DateTime UpdatedDate { get; set; }
}