using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Token.Request;
using EtherScan.Dotnet.Client.Models.Token.Response;

namespace EtherScan.Dotnet.Client.Tests.Token;

public class TokenTests : EtherScanClientTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;
    private const string TestContractAddress = "0x57d90b64a1a57749b0f932f1a3395792e12e7055";
    private const string TestAddress = "0xe04f27eb70e025b78871a2ad7eabe85e61212761";

    [Fact]
    public async Task GetErc20TokenSupplyAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<string>
        {
            Status = "1",
            Message = "OK",
            Result = "21265524714464"
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TokenSupplyRequest { ContractAddress = TestContractAddress };

        // Act
        var result = await client.GetErc20TokenSupplyAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("21265524714464", result.Result);
    }

    [Fact]
    public async Task GetErc20TokenBalanceAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<string>
        {
            Status = "1",
            Message = "OK",
            Result = "135499"
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TokenBalanceRequest 
        { 
            ContractAddress = TestContractAddress,
            Address = TestAddress
        };

        // Act
        var result = await client.GetErc20TokenBalanceAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("135499", result.Result);
    }

    [Fact]
    public async Task GetTokenInfoAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<TokenInfoResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<TokenInfoResponse>
            {
                new()
                {
                    ContractAddress = TestContractAddress,
                    TokenName = "Test Token",
                    Symbol = "TEST",
                    TokenType = "ERC20",
                    TotalSupply = "1000000",
                    BlueCheckmark = "true"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TokenSupplyRequest { ContractAddress = TestContractAddress };

        // Act
        var result = await client.GetTokenInfoAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal(TestContractAddress, result.Result[0].ContractAddress);
        Assert.Equal("Test Token", result.Result[0].TokenName);
    }
} 