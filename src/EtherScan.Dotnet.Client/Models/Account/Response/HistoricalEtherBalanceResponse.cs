namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class HistoricalEtherBalanceResponse
{
    [JsonPropertyName("result")]
    public required string Balance { get; set; }
}