namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class TransactionResponse
{
    [JsonPropertyName("blockNumber")]
    public required string BlockNumber { get; set; }

    [JsonPropertyName("blockHash")]
    public string? BlockHash { get; set; }

    [JsonPropertyName("timeStamp")]
    public required string TimeStamp { get; set; }

    [JsonPropertyName("hash")]
    public required string Hash { get; set; }

    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }

    [JsonPropertyName("transactionIndex")]
    public string? TransactionIndex { get; set; }

    [JsonPropertyName("from")]
    public required string From { get; set; }

    [JsonPropertyName("to")]
    public required string To { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("gas")]
    public required string Gas { get; set; }

    [JsonPropertyName("gasPrice")]
    public string? GasPrice { get; set; }

    [JsonPropertyName("input")]
    public required string Input { get; set; }

    [JsonPropertyName("methodId")]
    public string? MethodId { get; set; }

    [JsonPropertyName("functionName")]
    public string? FunctionName { get; set; }

    [JsonPropertyName("contractAddress")]
    public required string ContractAddress { get; set; }

    [JsonPropertyName("cumulativeGasUsed")]
    public string? CumulativeGasUsed { get; set; }

    [JsonPropertyName("txreceipt_status")]
    public string? TxReceiptStatus { get; set; }

    [JsonPropertyName("gasUsed")]
    public required string GasUsed { get; set; }

    [JsonPropertyName("confirmations")]
    public string? Confirmations { get; set; }

    [JsonPropertyName("isError")]
    public required string IsError { get; set; }
}