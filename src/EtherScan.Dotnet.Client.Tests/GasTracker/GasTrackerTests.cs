using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.GasTracker.Request;
using EtherScan.Dotnet.Client.Models.GasTracker.Response;

namespace EtherScan.Dotnet.Client.Tests.GasTracker;

public class GasTrackerTests : EtherScanClientTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;

    [Fact]
    public async Task GetEstimationOfConfirmationTimeAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<GasEstimateResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new GasEstimateResponse
            {
                Result = "9227"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new GasEstimateRequest { GasPrice = "2000000000" };

        // Act
        var result = await client.GetEstimationOfConfirmationTimeAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("9227", result.Result.Result);
    }

    [Fact]
    public async Task GetGasOracleAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<GasOracleResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new GasOracleResponse
            {
                LastBlock = "13053741",
                SafeGasPrice = "20",
                ProposeGasPrice = "22",
                FastGasPrice = "24",
                SuggestBaseFee = "19.230609716",
                GasUsedRatio = "0.370119078777807,0.8954731,0.550911766666667,0.212457033333333,0.552463633333333"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));

        // Act
        var result = await client.GetGasOracleAsync();

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("13053741", result.Result.LastBlock);
        Assert.Equal("20", result.Result.SafeGasPrice);
    }

    [Fact]
    public async Task GetDailyAverageGasLimitAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<DailyGasLimitResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<DailyGasLimitResponse>
            {
                new()
                {
                    UtcDate = "2019-02-01",
                    UnixTimeStamp = "1548979200",
                    GasLimit = "8001360"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new DailyGasStatsRequest
        {
            StartDate = "2019-02-01",
            EndDate = "2019-02-28",
            Sort = "asc"
        };

        // Act
        var result = await client.GetDailyAverageGasLimitAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal("2019-02-01", result.Result[0].UtcDate);
        Assert.Equal("8001360", result.Result[0].GasLimit);
    }

    [Fact]
    public async Task GetDailyTotalGasUsedAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<DailyGasUsedResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<DailyGasUsedResponse>
            {
                new()
                {
                    UtcDate = "2019-02-01",
                    UnixTimeStamp = "1548979200",
                    GasUsed = "32761450415"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new DailyGasStatsRequest
        {
            StartDate = "2019-02-01",
            EndDate = "2019-02-28",
            Sort = "asc"
        };

        // Act
        var result = await client.GetDailyTotalGasUsedAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal("2019-02-01", result.Result[0].UtcDate);
        Assert.Equal("32761450415", result.Result[0].GasUsed);
    }

    [Fact]
    public async Task GetDailyAverageGasPriceAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<DailyAverageGasPriceResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<DailyAverageGasPriceResponse>
            {
                new()
                {
                    UtcDate = "2019-02-01",
                    UnixTimeStamp = "1548979200",
                    MaxGasPriceWei = "60814303896257",
                    MinGasPriceWei = "432495",
                    AvgGasPriceWei = "13234562600"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new DailyGasStatsRequest
        {
            StartDate = "2019-02-01",
            EndDate = "2019-02-28",
            Sort = "asc"
        };

        // Act
        var result = await client.GetDailyAverageGasPriceAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal("2019-02-01", result.Result[0].UtcDate);
        Assert.Equal("60814303896257", result.Result[0].MaxGasPriceWei);
        Assert.Equal("432495", result.Result[0].MinGasPriceWei);
        Assert.Equal("13234562600", result.Result[0].AvgGasPriceWei);
    }
} 