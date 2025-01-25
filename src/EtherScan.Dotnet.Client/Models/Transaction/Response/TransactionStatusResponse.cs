namespace EtherScan.Dotnet.Client.Models.Transaction.Response;

public class TransactionStatusResponse
{
    [JsonPropertyName("isError")]
    public required string IsError { get; set; }
    
    [JsonPropertyName("errDescription")]
    public string? ErrDescription { get; set; }
}