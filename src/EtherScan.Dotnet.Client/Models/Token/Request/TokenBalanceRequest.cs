using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Token.Request;

public class TokenBalanceRequest : IUrlParameterizable
{
    public required string ContractAddress { get; set; }
    public required string Address { get; set; }
    public string Tag { get; set; } = "latest";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&contractaddress=").Append(ContractAddress)
            .Append("&address=").Append(Address)
            .Append("&tag=").Append(Tag);
    }
} 