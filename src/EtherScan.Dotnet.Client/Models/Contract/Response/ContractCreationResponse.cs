namespace EtherScan.Dotnet.Client.Models.Contract.Response;

public class ContractCreationResponse
{
    [JsonPropertyName("contractAddress")]
    public string ContractAddress { get; set; } = string.Empty;

    [JsonPropertyName("contractCreator")]
    public string ContractCreator { get; set; } = string.Empty;

    [JsonPropertyName("txHash")]
    public string TxHash { get; set; } = string.Empty;
    
    [JsonPropertyName("blockNumber")]
    public string BlockNumber { get; set; } = string.Empty;
    
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; } = string.Empty;
    
    [JsonPropertyName("contractFactory")]
    public string ContractFactory { get; set; } = string.Empty;
    
    [JsonPropertyName("creationBytecode")]
    public string CreationBytecode { get; set; } = string.Empty;
}