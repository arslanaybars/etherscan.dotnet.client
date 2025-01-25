namespace EtherScan.Dotnet.Client.Models.ChainLogs.Response;

public class LogResponse
{
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    [JsonPropertyName("topics")]
    public required List<string> Topics { get; set; }

    [JsonPropertyName("data")]
    public required string Data { get; set; }

    [JsonPropertyName("blockNumber")]
    public required string BlockNumber { get; set; }

    [JsonPropertyName("timeStamp")]
    public required string TimeStamp { get; set; }

    [JsonPropertyName("gasPrice")]
    public required string GasPrice { get; set; }

    [JsonPropertyName("gasUsed")]
    public required string GasUsed { get; set; }

    [JsonPropertyName("logIndex")]
    public required string LogIndex { get; set; }

    [JsonPropertyName("transactionHash")]
    public required string TransactionHash { get; set; }

    [JsonPropertyName("transactionIndex")]
    public required string TransactionIndex { get; set; }
}