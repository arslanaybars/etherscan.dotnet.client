namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class DailyBlockRewardsResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("blockRewards_Eth")]
    public required string BlockRewardsEth { get; set; }
} 