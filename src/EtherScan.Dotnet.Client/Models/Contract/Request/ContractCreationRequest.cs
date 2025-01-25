using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Contract.Request;

public class ContractCreationRequest : IUrlParameterizable
{
    [JsonPropertyName("contractaddresses")]
    public required string ContractAddresses { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&contractaddresses=").Append(ContractAddresses);
    }
}