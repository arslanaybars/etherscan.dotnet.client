namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class Erc721TokenTransferEventResponse : TransactionResponse
{
    [JsonPropertyName("tokenID")]
    public required string TokenId { get; set; }

    [JsonPropertyName("tokenName")]
    public required string TokenName { get; set; }

    [JsonPropertyName("tokenSymbol")]
    public required string TokenSymbol { get; set; }

    [JsonPropertyName("tokenDecimal")]
    public required string TokenDecimal { get; set; }
}