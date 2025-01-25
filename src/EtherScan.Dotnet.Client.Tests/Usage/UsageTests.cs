using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Usage.Response;

namespace EtherScan.Dotnet.Client.Tests.Usage;

public class UsageTests : EtherScanClientTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;

    [Fact]
    public async Task GetApiCreditUsageAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<ApiLimitResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new ApiLimitResponse
            {
                CreditsUsed = 207,
                CreditsAvailable = 499793,
                CreditLimit = 500000,
                LimitInterval = "daily",
                IntervalExpiryTimespan = "07:20:05"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));

        // Act
        var result = await client.GetApiCreditUsageAsync();

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(207, result.Result.CreditsUsed);
        Assert.Equal(499793, result.Result.CreditsAvailable);
        Assert.Equal(500000, result.Result.CreditLimit);
        Assert.Equal("daily", result.Result.LimitInterval);
        Assert.Equal("07:20:05", result.Result.IntervalExpiryTimespan);
    }

    [Fact]
    public async Task GetSupportedChainsAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new ChainListResponse
        {
            TotalCount = 2,
            Result = new List<ChainInfoResponse>
            {
                new()
                {
                    ChainName = "Ethereum Mainnet",
                    ChainId = "1",
                    BlockExplorer = "https://etherscan.io",
                    ApiUrl = "https://api.etherscan.io/v2/api?chainid=1",
                    Status = 1
                },
                new()
                {
                    ChainName = "Sepolia Testnet",
                    ChainId = "11155111",
                    BlockExplorer = "https://sepolia.etherscan.io",
                    ApiUrl = "https://api.etherscan.io/v2/api?chainid=11155111",
                    Status = 1
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));

        // Act
        var result = await client.GetSupportedChainsAsync();

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Result.Count);
        Assert.Equal("Ethereum Mainnet", result.Result[0].ChainName);
        Assert.Equal("1", result.Result[0].ChainId);
        Assert.Equal("Sepolia Testnet", result.Result[1].ChainName);
        Assert.Equal("11155111", result.Result[1].ChainId);
    }
} 