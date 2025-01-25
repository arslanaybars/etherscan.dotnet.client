using System.Text.Json.Serialization;

namespace EtherScan.Dotnet.Client.Models.Usage.Response;

public class ApiLimitResponse
{
    [JsonPropertyName("creditsUsed")]
    public required int CreditsUsed { get; set; }

    [JsonPropertyName("creditsAvailable")]
    public required int CreditsAvailable { get; set; }

    [JsonPropertyName("creditLimit")]
    public required int CreditLimit { get; set; }

    [JsonPropertyName("limitInterval")]
    public required string LimitInterval { get; set; }

    [JsonPropertyName("intervalExpiryTimespan")]
    public required string IntervalExpiryTimespan { get; set; }
} 