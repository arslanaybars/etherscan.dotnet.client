using EtherScan.Dotnet.Client.Models.Base;

namespace EtherScan.Dotnet.Client.Models.Account.Request
{
    public class Erc20TokenTransferEventRequest : IUrlParameterizable
    {
        public required string ContractAddress { get; set; }
        public required string Address { get; set; }
        public int Page { get; set; } = 1;
        public int Offset { get; set; } = 100;
        public int StartBlock { get; set; } = 0;
        public int EndBlock { get; set; } = 999999999;
        public string Sort { get; set; } = "asc";

        public void AppendUrlParameters(StringBuilder urlBuilder)
        {
            urlBuilder.Append("&contractaddress=").Append(ContractAddress)
                .Append("&address=").Append(Address)
                .Append("&page=").Append(Page)
                .Append("&offset=").Append(Offset)
                .Append("&startblock=").Append(StartBlock)
                .Append("&endblock=").Append(EndBlock)
                .Append("&sort=").Append(Sort);
        }
    }
}