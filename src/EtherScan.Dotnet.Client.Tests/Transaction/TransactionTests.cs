using System.Net;
using System.Text.Json;
using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Transaction.Request;
using EtherScan.Dotnet.Client.Models.Transaction.Response;
using Moq;
using Moq.Protected;

namespace EtherScan.Dotnet.Client.Tests.Transaction;

public class TransactionTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;
    private const string TestTxHash = "0x15f8e5ea1079d9a0bb04a4c58ae5fe7654b5b2b4463375ff7ffb490aa0032f3a";

    [Fact]
    public async Task GetContractExecutionStatusAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<TransactionStatusResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new TransactionStatusResponse
            {
                IsError = "1",
                ErrDescription = "Bad jump destination"
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
        var request = new TransactionStatusRequest
        {
            TxHash = TestTxHash
        };

        // Act
        var result = await client.GetContractExecutionStatusAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("1", result.Result?.IsError);
        Assert.Equal("Bad jump destination", result.Result?.ErrDescription);
    }

    [Fact]
    public async Task GetTransactionReceiptStatusAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<TransactionReceiptStatusResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new TransactionReceiptStatusResponse
            {
                Status = "1"
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
        var request = new TransactionStatusRequest
        {
            TxHash = TestTxHash
        };

        // Act
        var result = await client.GetTransactionReceiptStatusAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("1", result.Result?.Status);
    }

    [Fact]
    public async Task GetContractExecutionStatusAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TransactionStatusRequest { TxHash = TestTxHash };

        // Act
        var result = await client.GetContractExecutionStatusAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetTransactionReceiptStatusAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TransactionStatusRequest { TxHash = TestTxHash };

        // Act
        var result = await client.GetTransactionReceiptStatusAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }
} 