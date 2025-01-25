using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.ChainLogs.Request;

public class LogRequest : IUrlParameterizable
{
    public string? Address { get; set; }
    public required string FromBlock { get; set; }
    public required string ToBlock { get; set; }
    public string? Topic0 { get; set; }
    public string? Topic1 { get; set; }
    public string? Topic2 { get; set; }
    public string? Topic3 { get; set; }
    public string? Topic0_1_Opr { get; set; }
    public string? Topic1_2_Opr { get; set; }
    public string? Topic2_3_Opr { get; set; }
    public string? Topic0_2_Opr { get; set; }
    public string? Topic0_3_Opr { get; set; }
    public string? Topic1_3_Opr { get; set; }
    public int Page { get; set; } = 1;
    public int Offset { get; set; } = 1000;

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        if (!string.IsNullOrEmpty(Address))
            urlBuilder.Append("&address=").Append(Uri.EscapeDataString(Address));
            
        urlBuilder.Append("&fromBlock=").Append(Uri.EscapeDataString(FromBlock))
            .Append("&toBlock=").Append(Uri.EscapeDataString(ToBlock));

        if (!string.IsNullOrEmpty(Topic0))
            urlBuilder.Append("&topic0=").Append(Uri.EscapeDataString(Topic0));
        if (!string.IsNullOrEmpty(Topic1))
            urlBuilder.Append("&topic1=").Append(Uri.EscapeDataString(Topic1));
        if (!string.IsNullOrEmpty(Topic2))
            urlBuilder.Append("&topic2=").Append(Uri.EscapeDataString(Topic2));
        if (!string.IsNullOrEmpty(Topic3))
            urlBuilder.Append("&topic3=").Append(Uri.EscapeDataString(Topic3));

        if (!string.IsNullOrEmpty(Topic0_1_Opr))
            urlBuilder.Append("&topic0_1_opr=").Append(Uri.EscapeDataString(Topic0_1_Opr));
        if (!string.IsNullOrEmpty(Topic1_2_Opr))
            urlBuilder.Append("&topic1_2_opr=").Append(Uri.EscapeDataString(Topic1_2_Opr));
        if (!string.IsNullOrEmpty(Topic2_3_Opr))
            urlBuilder.Append("&topic2_3_opr=").Append(Uri.EscapeDataString(Topic2_3_Opr));
        if (!string.IsNullOrEmpty(Topic0_2_Opr))
            urlBuilder.Append("&topic0_2_opr=").Append(Uri.EscapeDataString(Topic0_2_Opr));
        if (!string.IsNullOrEmpty(Topic0_3_Opr))
            urlBuilder.Append("&topic0_3_opr=").Append(Uri.EscapeDataString(Topic0_3_Opr));
        if (!string.IsNullOrEmpty(Topic1_3_Opr))
            urlBuilder.Append("&topic1_3_opr=").Append(Uri.EscapeDataString(Topic1_3_Opr));

        urlBuilder.Append("&page=").Append(Page)
            .Append("&offset=").Append(Offset);
    }
}