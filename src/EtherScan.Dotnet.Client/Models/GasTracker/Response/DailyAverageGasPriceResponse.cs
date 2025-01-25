namespace EtherScan.Dotnet.Client.Models.GasTracker.Response;

public class DailyAverageGasPriceResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("maxGasPrice_Wei")]
    public required string MaxGasPriceWei { get; set; }

    [JsonPropertyName("minGasPrice_Wei")]
    public required string MinGasPriceWei { get; set; }

    [JsonPropertyName("avgGasPrice_Wei")]
    public required string AvgGasPriceWei { get; set; }
} 