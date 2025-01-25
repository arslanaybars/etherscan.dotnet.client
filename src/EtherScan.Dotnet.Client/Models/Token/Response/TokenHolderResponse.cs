namespace EtherScan.Dotnet.Client.Models.Token.Response;

public class TokenHolderResponse
{
    [JsonPropertyName("TokenHolderAddress")]
    public string TokenHolderAddress { get; set; } = string.Empty;

    [JsonPropertyName("TokenHolderQuantity")]
    public string TokenHolderQuantity { get; set; } = string.Empty;
} 