using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Token.Request;

public class TokenHolderListRequest : IUrlParameterizable
{
    public required string ContractAddress { get; set; }
    public int Page { get; set; } = 1;
    public int Offset { get; set; } = 10;

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&contractaddress=").Append(ContractAddress)
            .Append("&page=").Append(Page)
            .Append("&offset=").Append(Offset);
    }
} 