using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class AccountBalanceRequest : IUrlParameterizable
{
    public required string Address { get; set; }
    public string Tag { get; set; } = "latest";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&address=")
            .Append(Uri.EscapeDataString(Address))
            .Append("&tag=")
            .Append(Uri.EscapeDataString(Tag));
    }
}