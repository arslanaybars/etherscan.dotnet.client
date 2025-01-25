namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class Erc1155TokenTransferEventResponse : TransactionResponse
{
    [JsonPropertyName("tokenID")]
    public required string TokenId { get; set; }

    [JsonPropertyName("tokenValue")]
    public required string TokenValue { get; set; }

    [JsonPropertyName("tokenName")]
    public required string TokenName { get; set; }

    [JsonPropertyName("tokenSymbol")]
    public required string TokenSymbol { get; set; }
}