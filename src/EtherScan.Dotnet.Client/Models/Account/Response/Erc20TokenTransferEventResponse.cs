namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class Erc20TokenTransferEventResponse : TransactionResponse
{
    [JsonPropertyName("tokenName")]
    public required string TokenName { get; set; }

    [JsonPropertyName("tokenSymbol")]
    public required string TokenSymbol { get; set; }

    [JsonPropertyName("tokenDecimal")]
    public required string TokenDecimal { get; set; }
}