using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class HistoricalEtherBalanceRequest : IUrlParameterizable
{
    public required string Address { get; set; }
    public required int BlockNo { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&address=").Append(Address)
            .Append("&blockno=").Append(BlockNo);
    }
}