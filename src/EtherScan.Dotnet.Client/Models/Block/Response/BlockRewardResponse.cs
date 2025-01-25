namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class BlockRewardResponse
{
    [JsonPropertyName("blockNumber")]
    public required string BlockNumber { get; set; }

    [JsonPropertyName("timeStamp")]
    public required string TimeStamp { get; set; }

    [JsonPropertyName("blockMiner")]
    public required string BlockMiner { get; set; }

    [JsonPropertyName("blockReward")]
    public required string BlockReward { get; set; }

    [JsonPropertyName("uncles")]
    public required List<UncleBlock> Uncles { get; set; }

    [JsonPropertyName("uncleInclusionReward")]
    public required string UncleInclusionReward { get; set; }
}

public class UncleBlock
{
    [JsonPropertyName("miner")]
    public required string Miner { get; set; }

    [JsonPropertyName("unclePosition")]
    public required string UnclePosition { get; set; }

    [JsonPropertyName("blockreward")]
    public required string BlockReward { get; set; }
} 