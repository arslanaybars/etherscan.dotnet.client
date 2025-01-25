namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class BeaconChainWithdrawalResponse
{
    [JsonPropertyName("withdrawalIndex")]
    public required string WithdrawalIndex { get; set; }

    [JsonPropertyName("validatorIndex")]
    public required string ValidatorIndex { get; set; }

    [JsonPropertyName("address")]
    public required string Address { get; set; }

    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("blockNumber")]
    public required string BlockNumber { get; set; }

    [JsonPropertyName("timestamp")]
    public required string Timestamp { get; set; }
}