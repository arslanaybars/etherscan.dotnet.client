using EtherScan.Dotnet.Client.Models.Account.Request;
using EtherScan.Dotnet.Client.Models.Account.Response;
using EtherScan.Dotnet.Client.Models.Base;
using Moq;
using Moq.Protected;

namespace EtherScan.Dotnet.Client.Tests.Account;

public class AccountTests : EtherScanClientTests
{
    private const string ApiKey = "test-api-key";
    private const int ChainId = 1;
    private const string TestAddress = "0x123";

    [Fact]
    public async Task GetAccountBalanceAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<decimal>
        {
            Status = "1",
            Message = "OK",
            Result = 1000000000000000000 // 1 ETH in Wei
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));

        // Act
        var result = await client.GetAccountBalanceAsync(new AccountBalanceRequest { Address = TestAddress });

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(1000000000000000000m, result.Result?.Balance);
        Assert.Equal(1m, result.Result?.BalanceInEther);
    }

    [Fact]
    public async Task GetMultipleAccountBalanceAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<AccountBalanceResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<AccountBalanceResponse>
            {
                new() { Account = "0x123", Balance = 1000000000000000000m },
                new() { Account = "0x456", Balance = 2000000000000000000m }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));

        // Act
        var result = await client.GetMultipleAccountBalanceAsync(
            new AccountBalanceRequest { Address = "0x123,0x456" });

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal(1m, result.Result?[0].BalanceInEther);
        Assert.Equal(2m, result.Result?[1].BalanceInEther);
    }

    [Fact]
    public async Task GetAccountBalanceAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));

        // Act
        var result = await client.GetAccountBalanceAsync(new AccountBalanceRequest { Address = TestAddress });

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetMultipleAccountBalanceAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));

        // Act
        var result = await client.GetMultipleAccountBalanceAsync(
            new AccountBalanceRequest { Address = "0x123,0x456" });

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetAccountBalanceAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));

        // Act
        var result = await client.GetAccountBalanceAsync(new AccountBalanceRequest { Address = TestAddress });

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetMultipleAccountBalanceAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));

        // Act
        var result = await client.GetMultipleAccountBalanceAsync(
            new AccountBalanceRequest { Address = "0x123,0x456" });

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetTransactionsByAddressAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<TransactionResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<TransactionResponse>
            {
                new()
                {
                    BlockNumber = "12345",
                    TimeStamp = "1622222222",
                    Hash = "0xabc123",
                    From = "0x123",
                    To = "0x456",
                    Value = "1000000000000000000", // 1 ETH
                    Gas = "21000",
                    GasPrice = "20000000000",
                    IsError = "0",
                    TxReceiptStatus = "1",
                    BlockHash = "",
                    Nonce = "",
                    TransactionIndex = "",
                    Input = "",
                    MethodId = "",
                    FunctionName = "",
                    ContractAddress = "",
                    CumulativeGasUsed = "",
                    GasUsed = "",
                    Confirmations = ""
                },
                new()
                {
                    BlockNumber = "12346",
                    TimeStamp = "1622222223",
                    Hash = "0xdef456",
                    From = "0x456",
                    To = "0x123",
                    Value = "2000000000000000000", // 2 ETH
                    Gas = "21000",
                    GasPrice = "20000000000",
                    IsError = "0",
                    TxReceiptStatus = "1",
                    BlockHash = "",
                    Nonce = "",
                    TransactionIndex = "",
                    Input = "",
                    MethodId = "",
                    FunctionName = "",
                    ContractAddress = "",
                    CumulativeGasUsed = "",
                    GasUsed = "",
                    Confirmations = ""
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("0xabc123", result.Result?[0].Hash);
        Assert.Equal("0xdef456", result.Result?[1].Hash);
        Assert.Equal("1000000000000000000", result.Result?[0].Value);
        Assert.Equal("2000000000000000000", result.Result?[1].Value);
    }

    [Fact]
    public async Task GetTransactionsByAddressAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetTransactionsByAddressAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetTransactionsByAddressAsync_WithEmptyResponse_ReturnsEmptyList()
    {
        // Arrange
        var expectedResponse = new Response<List<TransactionResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<TransactionResponse>()
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Result!);
    }

    [Fact]
    public async Task GetInternalTransactionsByAddressAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<TransactionResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<TransactionResponse>
            {
                new()
                {
                    BlockNumber = "12345",
                    TimeStamp = "1622222222",
                    Hash = "0xabc123",
                    From = "0x123",
                    To = "0x456",
                    Value = "1000000000000000000", // 1 ETH
                    Gas = "21000",
                    GasPrice = "20000000000",
                    IsError = "0",
                    TxReceiptStatus = "1",
                    BlockHash = "",
                    Input = "",
                    ContractAddress = "",
                    GasUsed = ""
                },
                new()
                {
                    BlockNumber = "12346",
                    TimeStamp = "1622222223",
                    Hash = "0xdef456",
                    From = "0x456",
                    To = "0x123",
                    Value = "2000000000000000000", // 2 ETH
                    Gas = "21000",
                    GasPrice = "20000000000",
                    IsError = "0",
                    TxReceiptStatus = "1",
                    BlockHash = "",
                    Input = "",
                    ContractAddress = "",
                    GasUsed = ""
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetInternalTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("0xabc123", result.Result?[0].Hash);
        Assert.Equal("0xdef456", result.Result?[1].Hash);
        Assert.Equal("1000000000000000000", result.Result?[0].Value);
        Assert.Equal("2000000000000000000", result.Result?[1].Value);
    }

    [Fact]
    public async Task GetInternalTransactionsByAddressAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetInternalTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetInternalTransactionsByAddressAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TransactionRequest
        {
            Address = TestAddress,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetInternalTransactionsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetInternalTransactionsByTransactionHashAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<TransactionResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<TransactionResponse>
            {
                new()
                {
                    BlockNumber = "1743059",
                    TimeStamp = "1466489498",
                    From = "0x2cac6e4b11d6b58f6d3c1c9d5fe8faa89f60e5a2",
                    To = "0x66a1c3eaf0f1ffc28d209c0763ed0ca614f3b002",
                    Value = "7106740000000000",
                    ContractAddress = "",
                    Input = "",
                    Gas = "2300",
                    GasUsed = "0",
                    IsError = "0",
                    Hash = "abc"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new TxnTransactionRequest
        {
            TransactionHash = "0x40eb908387324f2b575b4879cd9d7188f69c8fc9d87c901b9e2daaea4b442170"
        };

        // Act
        var result = await client.GetInternalTransactionsByTransactionHashAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal("1743059", result.Result?[0].BlockNumber);
        Assert.Equal("0x2cac6e4b11d6b58f6d3c1c9d5fe8faa89f60e5a2", result.Result?[0].From);
        Assert.Equal("0x66a1c3eaf0f1ffc28d209c0763ed0ca614f3b002", result.Result?[0].To);
        Assert.Equal("7106740000000000", result.Result?[0].Value);
    }

    [Fact]
    public async Task GetInternalTransactionsByTransactionHashAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TxnTransactionRequest
        {
            TransactionHash = "0x40eb908387324f2b575b4879cd9d7188f69c8fc9d87c901b9e2daaea4b442170"
        };

        // Act
        var result = await client.GetInternalTransactionsByTransactionHashAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetInternalTransactionsByTransactionHashAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new TxnTransactionRequest
        {
            TransactionHash = "0x40eb908387324f2b575b4879cd9d7188f69c8fc9d87c901b9e2daaea4b442170"
        };

        // Act
        var result = await client.GetInternalTransactionsByTransactionHashAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetInternalTransactionsByBlockRangeAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<TransactionResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<TransactionResponse>
            {
                new()
                {
                    BlockNumber = "50107",
                    TimeStamp = "1438984016",
                    Hash = "0x3f97c969ddf71f515ce5373b1f8e76e9fd7016611d8ce455881009414301789e",
                    From = "0x109c4f2ccc82c4d77bde15f306707320294aea3f",
                    To = "0x881b0a4e9c55d08e31d8d3c022144d75a454211c",
                    Value = "1000000000000000000",
                    ContractAddress = "",
                    Input = "",
                    Gas = "2300",
                    GasUsed = "0",
                    IsError = "1",
                },
                new()
                {
                    BlockNumber = "50111",
                    TimeStamp = "1438984075",
                    Hash = "0x893c428fed019404f704cf4d9be977ed9ca01050ed93dccdd6c169422155586f",
                    From = "0x109c4f2ccc82c4d77bde15f306707320294aea3f",
                    To = "0x881b0a4e9c55d08e31d8d3c022144d75a454211c",
                    Value = "1000000000000000000",
                    ContractAddress = "",
                    Input = "",
                    Gas = "2300",
                    GasUsed = "0",
                    IsError = "0",
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new BlockRangeRequest
        {
            StartBlock = 13481773,
            EndBlock = 13491773,
            Page = 1,
            Offset = 10,
            Sort = "asc"
        };

        // Act
        var result = await client.GetInternalTransactionsByBlockRangeAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("50107", result.Result?[0].BlockNumber);
        Assert.Equal("0x109c4f2ccc82c4d77bde15f306707320294aea3f", result.Result?[0].From);
        Assert.Equal("0x881b0a4e9c55d08e31d8d3c022144d75a454211c", result.Result?[0].To);
        Assert.Equal("1000000000000000000", result.Result?[0].Value);
    }

    [Fact]
    public async Task GetInternalTransactionsByBlockRangeAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new BlockRangeRequest
        {
            StartBlock = 13481773,
            EndBlock = 13491773,
            Page = 1,
            Offset = 10,
            Sort = "asc"
        };

        // Act
        var result = await client.GetInternalTransactionsByBlockRangeAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetInternalTransactionsByBlockRangeAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new BlockRangeRequest
        {
            StartBlock = 13481773,
            EndBlock = 13491773,
            Page = 1,
            Offset = 10,
            Sort = "asc"
        };

        // Act
        var result = await client.GetInternalTransactionsByBlockRangeAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetErc20TokenTransferEventsByAddressAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<Erc20TokenTransferEventResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<Erc20TokenTransferEventResponse>
            {
                new()
                {
                    BlockNumber = "4730207",
                    TimeStamp = "1513240363",
                    Hash = "0xe8c208398bd5ae8e4c237658580db56a2a94dfa0ca382c99b776fa6e7d31d5b4",
                    Nonce = "406",
                    BlockHash = "0x022c5e6a3d2487a8ccf8946a2ffb74938bf8e5c8a3f6d91b41c56378a96b5c37",
                    From = "0x642ae78fafbb8032da552d619ad43f1d81e4dd7c",
                    ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2",
                    To = "0x4e83362442b8d1bec281594cea3050c8eb01311c",
                    Value = "5901522149285533025181",
                    TokenName = "Maker",
                    TokenSymbol = "MKR",
                    TokenDecimal = "18",
                    TransactionIndex = "81",
                    Gas = "940000",
                    GasPrice = "32010000000",
                    GasUsed = "77759",
                    CumulativeGasUsed = "2523379",
                    Input = "deprecated",
                    Confirmations = "7968350",
                    IsError = "0"
                },
                new()
                {
                    BlockNumber = "4764973",
                    TimeStamp = "1513764636",
                    Hash = "0x9c82e89b7f6a4405d11c361adb6d808d27bcd9db3b04b3fb3bc05d182bbc5d6f",
                    Nonce = "428",
                    BlockHash = "0x87a4d04a6d8fce7a149e9dc528b88dc0c781a87456910c42984bdc15930a2cac",
                    From = "0x4e83362442b8d1bec281594cea3050c8eb01311c",
                    ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2",
                    To = "0x69076e44a9c70a67d5b79d95795aba299083c275",
                    Value = "132520488141080",
                    TokenName = "Maker",
                    TokenSymbol = "MKR",
                    TokenDecimal = "18",
                    TransactionIndex = "167",
                    Gas = "940000",
                    GasPrice = "35828000000",
                    GasUsed = "127593",
                    CumulativeGasUsed = "6315818",
                    Input = "deprecated",
                    Confirmations = "7933584",
                    IsError = "0"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new Erc20TokenTransferEventRequest
        {
            ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2",
            Address = "0x4e83362442b8d1bec281594cea3050c8eb01311c",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 27025780,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc20TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("4730207", result.Result?[0].BlockNumber);
        Assert.Equal("0x642ae78fafbb8032da552d619ad43f1d81e4dd7c", result.Result?[0].From);
        Assert.Equal("0x4e83362442b8d1bec281594cea3050c8eb01311c", result.Result?[0].To);
        Assert.Equal("5901522149285533025181", result.Result?[0].Value);
    }

    [Fact]
    public async Task GetErc20TokenTransferEventsByAddressAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new Erc20TokenTransferEventRequest
        {
            ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2",
            Address = "0x4e83362442b8d1bec281594cea3050c8eb01311c",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 27025780,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc20TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetErc20TokenTransferEventsByAddressAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new Erc20TokenTransferEventRequest
        {
            ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2",
            Address = "0x4e83362442b8d1bec281594cea3050c8eb01311c",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 27025780,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc20TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetErc721TokenTransferEventsByAddressAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<Erc721TokenTransferEventResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<Erc721TokenTransferEventResponse>
            {
                new()
                {
                    BlockNumber = "4708120",
                    TimeStamp = "1512907118",
                    Hash = "0x031e6968a8de362e4328d60dcc7f72f0d6fc84284c452f63176632177146de66",
                    Nonce = "0",
                    BlockHash = "0x4be19c278bfaead5cb0bc9476fa632e2447f6e6259e0303af210302d22779a24",
                    From = "0xb1690c08e213a35ed9bab7b318de14420fb57d8c",
                    ContractAddress = "0x06012c8cf97bead5deae237070f9587f8e7a266d",
                    To = "0x6975be450864c02b4613023c2152ee0743572325",
                    TokenId = "202106",
                    TokenName = "CryptoKitties",
                    TokenSymbol = "CK",
                    TokenDecimal = "0",
                    TransactionIndex = "81",
                    Gas = "158820",
                    GasPrice = "40000000000",
                    GasUsed = "60508",
                    CumulativeGasUsed = "4880352",
                    Input = "deprecated",
                    Confirmations = "7990490",
                    IsError = "0",
                    Value = ""
                },
                new()
                {
                    BlockNumber = "4708161",
                    TimeStamp = "1512907756",
                    Hash = "0x9626e7064b68b5463cf677e10815a0b394645a0bfa245f26a2de6074324e83ff",
                    Nonce = "1",
                    BlockHash = "0xe1c6cbc39a723496f4cbc3e70241012854f2e88b4d2d5f339d8f0a4a1cc406d8",
                    From = "0xb1690c08e213a35ed9bab7b318de14420fb57d8c",
                    ContractAddress = "0x06012c8cf97bead5deae237070f9587f8e7a266d",
                    To = "0x6975be450864c02b4613023c2152ee0743572325",
                    TokenId = "147739",
                    TokenName = "CryptoKitties",
                    TokenSymbol = "CK",
                    TokenDecimal = "0",
                    TransactionIndex = "41",
                    Gas = "135963",
                    GasPrice = "40000000000",
                    GasUsed = "45508",
                    CumulativeGasUsed = "3359342",
                    Input = "deprecated",
                    Confirmations = "7990449",
                    IsError = "0",
                    Value = ""
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new Erc721TokenTransferEventRequest
        {
            ContractAddress = "0x06012c8cf97bead5deae237070f9587f8e7a266d",
            Address = "0x6975be450864c02b4613023c2152ee0743572325",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 27025780,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc721TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("4708120", result.Result?[0].BlockNumber);
        Assert.Equal("0xb1690c08e213a35ed9bab7b318de14420fb57d8c", result.Result?[0].From);
        Assert.Equal("0x6975be450864c02b4613023c2152ee0743572325", result.Result?[0].To);
        Assert.Equal("202106", result.Result?[0].TokenId);
    }

    [Fact]
    public async Task GetErc721TokenTransferEventsByAddressAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new Erc721TokenTransferEventRequest
        {
            ContractAddress = "0x06012c8cf97bead5deae237070f9587f8e7a266d",
            Address = "0x6975be450864c02b4613023c2152ee0743572325",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 27025780,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc721TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetErc721TokenTransferEventsByAddressAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new Erc721TokenTransferEventRequest
        {
            ContractAddress = "0x06012c8cf97bead5deae237070f9587f8e7a266d",
            Address = "0x6975be450864c02b4613023c2152ee0743572325",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 27025780,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc721TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetErc1155TokenTransferEventsByAddressAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<Erc1155TokenTransferEventResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<Erc1155TokenTransferEventResponse>
            {
                new()
                {
                    BlockNumber = "13472395",
                    TimeStamp = "1634973285",
                    Hash = "0x643b15f3ffaad5d38e33e5872b4ebaa7a643eda8b50ffd5331f682934ee65d4d",
                    Nonce = "41",
                    BlockHash = "0xa5da536dfbe8125eb146114e2ee0d0bdef2b20483aacbf30fed6b60f092059e6",
                    From = "0x1e63326a84d2fa207bdfa856da9278a93deba418",
                    ContractAddress = "0x76be3b62873462d2142405439777e971754e8e77",
                    To = "0x83f564d180b58ad9a02a449105568189ee7de8cb",
                    TokenId = "10371",
                    TokenValue = "1",
                    TokenName = "parallel",
                    TokenSymbol = "LL",
                    Gas = "140000",
                    GasPrice = "52898577246",
                    GasUsed = "105030",
                    CumulativeGasUsed = "11739203",
                    Input = "deprecated",
                    Confirmations = "1447769",
                    IsError = "0"
                },
                new()
                {
                    BlockNumber = "14049909",
                    TimeStamp = "1642781541",
                    Hash = "0x58353aab15a4b5a77333b87619edaa749c7f3cf8bb2263a1c0865d73bf1264bd",
                    Nonce = "4",
                    BlockHash = "0x1e88a63a4cb4086a747644b8ab7ff3434540930f3029eacb8add08b15974fdc9",
                    From = "0x83f564d180b58ad9a02a449105568189ee7de8cb",
                    ContractAddress = "0x76be3b62873462d2142405439777e971754e8e77",
                    To = "0x80833dc92d326a81d0cb74982a8e6f1a3887f837",
                    TokenId = "10371",
                    TokenValue = "1",
                    TokenName = "parallel",
                    TokenSymbol = "LL",
                    Gas = "253032",
                    GasPrice = "225052869211",
                    GasUsed = "184899",
                    CumulativeGasUsed = "6855790",
                    Input = "deprecated",
                    Confirmations = "870255",
                    IsError = "0"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new Erc1155TokenTransferEventRequest
        {
            ContractAddress = "0x76be3b62873462d2142405439777e971754e8e77",
            Address = "0x83f564d180b58ad9a02a449105568189ee7de8cb",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc1155TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("13472395", result.Result?[0].BlockNumber);
        Assert.Equal("0x1e63326a84d2fa207bdfa856da9278a93deba418", result.Result?[0].From);
        Assert.Equal("0x83f564d180b58ad9a02a449105568189ee7de8cb", result.Result?[0].To);
        Assert.Equal("10371", result.Result?[0].TokenId);
    }

    [Fact]
    public async Task GetErc1155TokenTransferEventsByAddressAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new Erc1155TokenTransferEventRequest
        {
            ContractAddress = "0x76be3b62873462d2142405439777e971754e8e77",
            Address = "0x83f564d180b58ad9a02a449105568189ee7de8cb",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc1155TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetErc1155TokenTransferEventsByAddressAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new Erc1155TokenTransferEventRequest
        {
            ContractAddress = "0x76be3b62873462d2142405439777e971754e8e77",
            Address = "0x83f564d180b58ad9a02a449105568189ee7de8cb",
            Page = 1,
            Offset = 100,
            StartBlock = 0,
            EndBlock = 99999999,
            Sort = "asc"
        };

        // Act
        var result = await client.GetErc1155TokenTransferEventsByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetBlocksValidatedByAddressAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<MinedBlockResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<MinedBlockResponse>
            {
                new() { BlockNumber = "3462296", TimeStamp = "1491118514", BlockReward = "5194770940000000000" },
                new() { BlockNumber = "2691400", TimeStamp = "1480072029", BlockReward = "5086562212310617100" },
                new() { BlockNumber = "2687700", TimeStamp = "1480018852", BlockReward = "5003251945421042780" }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new MinedBlockRequest { Address = TestAddress };

        // Act
        var result = await client.GetBlocksValidatedByAddressAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Result?.Count);
        Assert.Equal("3462296", result.Result?[0].BlockNumber);
        Assert.Equal("1491118514", result.Result?[0].TimeStamp);
        Assert.Equal("5194770940000000000", result.Result?[0].BlockReward);
    }

    [Fact]
    public async Task GetBlocksValidatedByAddressAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new MinedBlockRequest { Address = TestAddress };

        // Act
        var result = await client.GetBlocksValidatedByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetBlocksValidatedByAddressAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new MinedBlockRequest { Address = TestAddress };

        // Act
        var result = await client.GetBlocksValidatedByAddressAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }


    [Fact]
    public async Task GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<BeaconChainWithdrawalResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<BeaconChainWithdrawalResponse>
            {
                new()
                {
                    WithdrawalIndex = "13", ValidatorIndex = "117823", Address = TestAddress, Amount = "3402931175",
                    BlockNumber = "17034877", Timestamp = "1681338599"
                },
                new()
                {
                    WithdrawalIndex = "14", ValidatorIndex = "119023", Address = TestAddress, Amount = "3244098967",
                    BlockNumber = "17034877", Timestamp = "1681338599"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new BeaconChainWithdrawalRequest { Address = TestAddress };

        // Act
        var result = await client.GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Result?.Count);
        Assert.Equal("13", result.Result?[0].WithdrawalIndex);
        Assert.Equal("117823", result.Result?[0].ValidatorIndex);
        Assert.Equal("3402931175", result.Result?[0].Amount);
    }

    [Fact]
    public async Task GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new BeaconChainWithdrawalRequest { Address = TestAddress };

        // Act
        var result = await client.GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));
        var request = new BeaconChainWithdrawalRequest { Address = TestAddress };

        // Act
        var result = await client.GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync(request);

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetHistoricalEtherBalanceByBlockNoAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<HistoricalEtherBalanceResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new HistoricalEtherBalanceResponse
            {
                Balance = "610538078574759898951277"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));

        // Act
        var result = await client.GetHistoricalEtherBalanceByBlockNoAsync(
            new HistoricalEtherBalanceRequest { Address = TestAddress, BlockNo = 1234 });

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("610538078574759898951277", result.Result?.Balance);
    }

    [Fact]
    public async Task GetHistoricalEtherBalanceByBlockNoAsync_HandlesHttpRequestException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));

        // Act
        var result = await client.GetHistoricalEtherBalanceByBlockNoAsync(
            new HistoricalEtherBalanceRequest { Address = TestAddress, BlockNo = 1234 });

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("HTTP request failed", result.Message);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetHistoricalEtherBalanceByBlockNoAsync_HandlesUnexpectedException()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Unexpected error"));

        var client = new EtherScanClient(ApiKey, ChainId, new HttpClient(mockHandler.Object));

        // Act
        var result = await client.GetHistoricalEtherBalanceByBlockNoAsync(
            new HistoricalEtherBalanceRequest { Address = TestAddress, BlockNo = 1234 });

        // Assert
        Assert.Equal("0", result.Status);
        Assert.Contains("Unexpected error", result.Message);
        Assert.False(result.IsSuccess);
    }
}