using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.GasTracker.Request;

public class GasEstimateRequest : IUrlParameterizable
{
    public required string GasPrice { get; set; }

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&gasprice=").Append(Uri.EscapeDataString(GasPrice));
    }
} 