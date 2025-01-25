using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Token.Request;

public class TokenSupplyRequest : IUrlParameterizable
{
    public required string ContractAddress { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&contractaddress=").Append(ContractAddress);
    }
} 