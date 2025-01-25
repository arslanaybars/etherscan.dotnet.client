using System.Net;
using System.Text.Json;
using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.ChainLogs.Request;
using EtherScan.Dotnet.Client.Models.ChainLogs.Response;
using Moq;
using Moq.Protected;

namespace EtherScan.Dotnet.Client.Tests.ChainLogs;

public class LogsTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;
    private const string TestAddress = "0xbd3531da5cf5857e7cfaa92426877b022e612cf8";

    [Fact]
    public async Task GetLogsAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<LogResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<LogResponse>
            {
                new()
                {
                    Address = TestAddress,
                    Topics = new List<string>
                    {
                        "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef",
                        "0x0000000000000000000000000000000000000000000000000000000000000000",
                        "0x000000000000000000000000c45a4b3b698f21f88687548e7f5a80df8b99d93d",
                        "0x00000000000000000000000000000000000000000000000000000000000000b5"
                    },
                    Data = "0x",
                    BlockNumber = "0xc48174",
                    TimeStamp = "0x60f9ce56",
                    GasPrice = "0x2e90edd000",
                    GasUsed = "0x247205",
                    LogIndex = "0x",
                    TransactionHash = "0x4ffd22d986913d33927a392fe4319bcd2b62f3afe1c15a2c59f77fc2cc4c20a9",
                    TransactionIndex = "0x"
                }
            }
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
            });

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new LogRequest
        {
            Address = TestAddress,
            FromBlock = "12878196",
            ToBlock = "12878196"
        };

        // Act
        var result = await client.GetLogsAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.Single(result.Result);
        Assert.Equal(TestAddress, result.Result[0].Address);
        Assert.Equal(4, result.Result[0].Topics.Count);
        Assert.Equal("0x", result.Result[0].Data);
    }

    [Fact]
    public async Task GetLogsAsync_WithTopics_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<LogResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<LogResponse>
            {
                new()
                {
                    Address = TestAddress,
                    Topics = new List<string>
                    {
                        "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef",
                        "0x0000000000000000000000000000000000000000000000000000000000000000"
                    },
                    Data = "0x",
                    BlockNumber = "0xc48174",
                    TimeStamp = "0x60f9ce56",
                    GasPrice = "0x2e90edd000",
                    GasUsed = "0x247205",
                    LogIndex = "0x",
                    TransactionHash = "0x4ffd22d986913d33927a392fe4319bcd2b62f3afe1c15a2c59f77fc2cc4c20a9",
                    TransactionIndex = "0x"
                }
            }
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
            });

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new LogRequest
        {
            FromBlock = "12878196",
            ToBlock = "12879196",
            Topic0 = "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef",
            Topic1 = "0x0000000000000000000000000000000000000000000000000000000000000000",
            Topic0_1_Opr = "and"
        };

        // Act
        var result = await client.GetLogsAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.Single(result.Result);
        Assert.Equal(2, result.Result[0].Topics.Count);
    }

    [Fact]
    public async Task GetLogsAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new LogRequest
        {
            Address = TestAddress,
            FromBlock = "12878196",
            ToBlock = "12878196"
        };

        // Act
        var result = await client.GetLogsAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetLogsAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new LogRequest
        {
            Address = TestAddress,
            FromBlock = "12878196",
            ToBlock = "12878196"
        };

        // Act
        var result = await client.GetLogsAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }
}