namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class BlockCountdownResponse
{
    [JsonPropertyName("CurrentBlock")]
    public required string CurrentBlock { get; set; }

    [JsonPropertyName("CountdownBlock")]
    public required string CountdownBlock { get; set; }

    [JsonPropertyName("RemainingBlock")]
    public required string RemainingBlock { get; set; }

    [JsonPropertyName("EstimateTimeInSec")]
    public required string EstimateTimeInSec { get; set; }
} 