using EtherScan.Dotnet.Client.Infrastructure.Converters;

namespace EtherScan.Dotnet.Client.Models.Account.Response;

public class AccountBalanceResponse
{
    public string Account { get; set; } = string.Empty;

    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Balance { get; set; }

    public decimal BalanceInEther => Balance / 1_000_000_000_000_000_000m;
}