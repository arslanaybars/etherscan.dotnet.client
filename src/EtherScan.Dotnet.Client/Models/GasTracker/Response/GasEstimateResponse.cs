namespace EtherScan.Dotnet.Client.Models.GasTracker.Response;

public class GasEstimateResponse
{
    [JsonPropertyName("result")]
    public required string Result { get; set; }
} 