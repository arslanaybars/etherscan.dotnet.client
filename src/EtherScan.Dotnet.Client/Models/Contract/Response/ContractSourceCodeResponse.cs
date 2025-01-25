namespace EtherScan.Dotnet.Client.Models.Contract.Response;

public class ContractSourceCodeResponse
{
    [JsonPropertyName("SourceCode")] public string SourceCode { get; set; }

    [JsonPropertyName("ABI")]
    public string Abi { get; set; } = string.Empty;

    [JsonPropertyName("ContractName")]
    public string ContractName { get; set; } = string.Empty;

    [JsonPropertyName("CompilerVersion")]
    public string CompilerVersion { get; set; } = string.Empty;

    [JsonPropertyName("OptimizationUsed")]
    public string OptimizationUsed { get; set; } = string.Empty;

    [JsonPropertyName("Runs")]
    public string Runs { get; set; } = string.Empty;

    [JsonPropertyName("ConstructorArguments")]
    public string ConstructorArguments { get; set; } = string.Empty;

    [JsonPropertyName("EVMVersion")]
    public string EvmVersion { get; set; } = string.Empty;

    [JsonPropertyName("Library")]
    public string Library { get; set; } = string.Empty;

    [JsonPropertyName("LicenseType")]
    public string LicenseType { get; set; } = string.Empty;

    [JsonPropertyName("Proxy")]
    public string Proxy { get; set; } = string.Empty;

    [JsonPropertyName("Implementation")]
    public string Implementation { get; set; } = string.Empty;

    [JsonPropertyName("SwarmSource")]
    public string SwarmSource { get; set; } = string.Empty;
}