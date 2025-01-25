namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class DailyUncleBlockCountAndRewardsResponse
{
    [JsonPropertyName("UTCDate")]
    public required string UtcDate { get; set; }

    [JsonPropertyName("unixTimeStamp")]
    public required string UnixTimeStamp { get; set; }

    [JsonPropertyName("uncleBlockCount")]
    public required string UncleBlockCount { get; set; }

    [JsonPropertyName("uncleBlockRewards_Eth")]
    public required string UncleBlockRewardsEth { get; set; }
} 