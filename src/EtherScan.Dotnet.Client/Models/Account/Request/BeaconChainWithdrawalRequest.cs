using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class BeaconChainWithdrawalRequest : IUrlParameterizable
{
    public required string Address { get; set; }
    public int StartBlock { get; set; } = 0;
    public int EndBlock { get; set; } = 99999999;
    public int Page { get; set; } = 1;
    public int Offset { get; set; } = 100;
    public string Sort { get; set; } = "asc";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&address=").Append(Address)
            .Append("&startblock=").Append(StartBlock)
            .Append("&endblock=").Append(EndBlock)
            .Append("&page=").Append(Page)
            .Append("&offset=").Append(Offset)
            .Append("&sort=").Append(Sort);
    }
}