using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Transaction.Request;

public class TransactionStatusRequest : IUrlParameterizable
{
    [JsonPropertyName("txhash")]
    public required string TxHash { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&txhash=")
            .Append(Uri.EscapeDataString(TxHash));
    }
}