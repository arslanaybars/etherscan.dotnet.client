using System.Text.Json.Serialization;

namespace EtherScan.Dotnet.Client.Models.Usage.Response;

public class ChainInfoResponse
{
    [JsonPropertyName("chainname")]
    public required string ChainName { get; set; }

    [JsonPropertyName("chainid")]
    public required string ChainId { get; set; }

    [JsonPropertyName("blockexplorer")]
    public required string BlockExplorer { get; set; }

    [JsonPropertyName("apiurl")]
    public required string ApiUrl { get; set; }

    [JsonPropertyName("status")]
    public required int Status { get; set; }
} 