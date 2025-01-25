namespace EtherScan.Dotnet.Client.Models.Base;

public class Request<T>
{
    public int ChainId { get; set; }
    public required string Module { get; set; }
    public required string Action { get; set; }
    public required string ApiKey { get; set; }
    public required T Parameters { get; set; }
}