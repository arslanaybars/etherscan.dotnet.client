using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request;

public class TransactionRequest : IUrlParameterizable
{
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    [JsonPropertyName("startblock")] 
    public int StartBlock { get; set; } = 0;

    [JsonPropertyName("endblock")]
    public int EndBlock { get; set; } = 99999999;

    [JsonPropertyName("page")]
    public int Page { get; set; } = 1;

    [JsonPropertyName("offset")] 
    public int Offset { get; set; } = 10;

    [JsonPropertyName("sort")]
    public string Sort { get; set; } = "asc";

    public void AppendUrlParameters(StringBuilder urlBuilder)
    {
        urlBuilder.Append("&address=")
            .Append(Uri.EscapeDataString(Address))
            .Append("&startblock=")
            .Append(StartBlock)
            .Append("&endblock=")
            .Append(EndBlock)
            .Append("&page=")
            .Append(Page)
            .Append("&offset=")
            .Append(Offset)
            .Append("&sort=")
            .Append(Uri.EscapeDataString(Sort));
    }
}