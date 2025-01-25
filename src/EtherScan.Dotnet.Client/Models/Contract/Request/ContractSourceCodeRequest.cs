using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Contract.Request;

public class ContractSourceCodeRequest : IUrlParameterizable
{
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&address=").Append(Address);
    }
}