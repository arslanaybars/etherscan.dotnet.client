namespace EtherScan.Dotnet.Client.Models.Contract.Response;

public class ContractAbiResponse
{
    [JsonPropertyName("abi")]
    public string Abi { get; set; }
}