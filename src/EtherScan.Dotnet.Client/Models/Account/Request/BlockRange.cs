using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class BlockRangeRequest : IUrlParameterizable
{
    public required int StartBlock { get; set; }
    public required int EndBlock { get; set; }
    public int Page { get; set; } = 1;
    public int Offset { get; set; } = 10;
    public string Sort { get; set; } = "asc";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&startblock=").Append(StartBlock)
            .Append("&endblock=").Append(EndBlock)
            .Append("&page=").Append(Page)
            .Append("&offset=").Append(Offset)
            .Append("&sort=").Append(Sort);
    }
}