namespace EtherScan.Dotnet.Client.Models.Transaction.Response;

public class TransactionReceiptStatusResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }
}