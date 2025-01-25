using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class TxnTransactionRequest : IUrlParameterizable
{
    public required string TransactionHash { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&txhash=").Append(TransactionHash);
    }
}