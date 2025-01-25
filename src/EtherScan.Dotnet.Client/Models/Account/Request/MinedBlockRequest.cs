using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class MinedBlockRequest : IUrlParameterizable
{
    public required string Address { get; set; }
    public string BlockType { get; set; } = "blocks";
    public int Page { get; set; } = 1;
    public int Offset { get; set; } = 10;

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&address=").Append(Address)
            .Append("&blocktype=").Append(BlockType)
            .Append("&page=").Append(Page)
            .Append("&offset=").Append(Offset);
    }
}