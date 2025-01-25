using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.GasTracker.Request;

public class DailyGasStatsRequest : IUrlParameterizable
{
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public string Sort { get; set; } = "asc";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&startdate=").Append(Uri.EscapeDataString(StartDate))
            .Append("&enddate=").Append(Uri.EscapeDataString(EndDate))
            .Append("&sort=").Append(Uri.EscapeDataString(Sort));
    }
} 