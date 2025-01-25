namespace EtherScan.Dotnet.Client.Models.GasTracker.Response;

public class GasOracleResponse
{
    [JsonPropertyName("LastBlock")]
    public required string LastBlock { get; set; }

    [JsonPropertyName("SafeGasPrice")]
    public required string SafeGasPrice { get; set; }

    [JsonPropertyName("ProposeGasPrice")]
    public required string ProposeGasPrice { get; set; }

    [JsonPropertyName("FastGasPrice")]
    public required string FastGasPrice { get; set; }

    [JsonPropertyName("suggestBaseFee")]
    public required string SuggestBaseFee { get; set; }

    [JsonPropertyName("gasUsedRatio")]
    public required string GasUsedRatio { get; set; }
} 