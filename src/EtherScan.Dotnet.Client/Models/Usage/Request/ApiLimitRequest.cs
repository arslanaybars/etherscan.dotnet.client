using System.Text;
using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Usage.Request;

public class ApiLimitRequest : IUrlParameterizable
{
    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        // No additional parameters needed for this endpoint
    }
} 