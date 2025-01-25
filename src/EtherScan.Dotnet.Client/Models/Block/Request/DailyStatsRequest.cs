using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Block.Request;

public class DailyStatsRequest : IUrlParameterizable
{
    [JsonPropertyName("startdate")]
    public required string StartDate { get; set; }

    [JsonPropertyName("enddate")]
    public required string EndDate { get; set; }

    [JsonPropertyName("sort")]
    public string Sort { get; set; } = "asc";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&startdate=").Append(StartDate)
            .Append("&enddate=").Append(EndDate)
            .Append("&sort=").Append(Sort);
    }
} 