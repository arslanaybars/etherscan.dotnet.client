using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.GasTracker.Request;

public class GasOracleRequest : IUrlParameterizable
{
    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        // No parameters needed for this endpoint
    }
} 