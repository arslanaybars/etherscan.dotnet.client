using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Block.Request;

public class BlockNumberByTimestampRequest : IUrlParameterizable
{
    [JsonPropertyName("timestamp")]
    public required int Timestamp { get; set; }

    [JsonPropertyName("closest")]
    public required string Closest { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&timestamp=").Append(Timestamp)
            .Append("&closest=").Append(Closest);
    }
} 