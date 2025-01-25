namespace EtherScan.Dotnet.Client.Models.GasTracker.Response;

public class DailyGasLimitResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("gasLimit")]
    public required string GasLimit { get; set; }
} 