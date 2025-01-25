namespace EtherScan.Dotnet.Client.Models.Token.Response;

public class TokenInfoResponse
{
    [JsonPropertyName("contractAddress")]
    public string ContractAddress { get; set; } = string.Empty;

    [JsonPropertyName("tokenName")]
    public string TokenName { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("divisor")]
    public string Divisor { get; set; } = string.Empty;

    [JsonPropertyName("tokenType")]
    public string TokenType { get; set; } = string.Empty;

    [JsonPropertyName("totalSupply")]
    public string TotalSupply { get; set; } = string.Empty;

    [JsonPropertyName("blueCheckmark")]
    public string BlueCheckmark { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("website")]
    public string Website { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("blog")]
    public string Blog { get; set; } = string.Empty;

    [JsonPropertyName("reddit")]
    public string Reddit { get; set; } = string.Empty;

    [JsonPropertyName("twitter")]
    public string Twitter { get; set; } = string.Empty;

    [JsonPropertyName("discord")]
    public string Discord { get; set; } = string.Empty;

    [JsonPropertyName("tokenPriceUSD")]
    public string TokenPriceUSD { get; set; } = string.Empty;
} 