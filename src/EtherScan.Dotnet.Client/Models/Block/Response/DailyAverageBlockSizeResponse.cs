namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class DailyAverageBlockSizeResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("blockSize_bytes")]
    public required string BlockSizeBytes { get; set; }
} 