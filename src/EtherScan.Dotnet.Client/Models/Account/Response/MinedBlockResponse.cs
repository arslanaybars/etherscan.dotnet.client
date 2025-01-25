namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class MinedBlockResponse
{
    [JsonPropertyName("blockNumber")]
    public required string BlockNumber { get; set; }

    [JsonPropertyName("timeStamp")]
    public required string TimeStamp { get; set; }

    [JsonPropertyName("blockReward")]
    public required string BlockReward { get; set; }
}