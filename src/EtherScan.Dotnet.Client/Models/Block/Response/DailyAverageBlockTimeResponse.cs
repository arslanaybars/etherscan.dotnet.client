namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class DailyAverageBlockTimeResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("blockTime_sec")]
    public required string BlockTimeSec { get; set; }
} 