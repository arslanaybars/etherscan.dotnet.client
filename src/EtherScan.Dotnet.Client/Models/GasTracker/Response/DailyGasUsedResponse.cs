namespace EtherScan.Dotnet.Client.Models.GasTracker.Response;

public class DailyGasUsedResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("gasUsed")]
    public required string GasUsed { get; set; }
} 