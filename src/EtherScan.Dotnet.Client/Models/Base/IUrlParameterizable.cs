namespace EtherScan.Dotnet.Client.Models.Base;

public interface IUrlParameterizable
{
    void AppendUrlParameters(StringBuilder urlBuilder);
}