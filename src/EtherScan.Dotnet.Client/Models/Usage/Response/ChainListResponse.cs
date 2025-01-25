using System.Text.Json.Serialization;

namespace EtherScan.Dotnet.Client.Models.Usage.Response;

public class ChainListResponse
{
    [JsonPropertyName("totalcount")]
    public required int TotalCount { get; set; }

    [JsonPropertyName("result")]
    public required List<ChainInfoResponse> Result { get; set; }
} 