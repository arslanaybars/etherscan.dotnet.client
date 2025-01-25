using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Block.Request;

public class BlockTransactionCountRequest : IUrlParameterizable
{
    [JsonPropertyName("blockno")]
    public required int BlockNo { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&blockno=").Append(BlockNo);
    }
} 