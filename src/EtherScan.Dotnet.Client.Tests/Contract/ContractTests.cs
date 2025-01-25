using System.Net;
using System.Text.Json;
using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Contract.Request;
using EtherScan.Dotnet.Client.Models.Contract.Response;
using Moq;
using Moq.Protected;

namespace EtherScan.Dotnet.Client.Tests.Contract;

public class ContractTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;

    [Fact]
    public async Task GetContractAbiAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<string>
        {
            Status = "1",
            Message = "OK",
            Result = "[{\"constant\":true,\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"type\":\"function\"}]"
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
        var request = new ContractAbiRequest
        {
            Address = "0xBB9bc244D798123fDe783fCc1C72d3Bb8C189413"
        };

        // Act
        var result = await client.GetContractAbiAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.Equal(expectedResponse.Result, result.Result);
    }

    [Fact]
    public async Task GetContractSourceCodeAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<ContractSourceCodeResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<ContractSourceCodeResponse>
            {
                new()
                {
                    SourceCode = "contract Test { }",
                    Abi = "[{\"constant\":true,\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"type\":\"function\"}]",
                    ContractName = "Test",
                    CompilerVersion = "v0.8.0",
                    OptimizationUsed = "1",
                    Runs = "200",
                    ConstructorArguments = "0000000000000000000000000000000000000000",
                    EvmVersion = "Default",
                    Library = "",
                    LicenseType = "MIT",
                    Proxy = "0",
                    Implementation = "",
                    SwarmSource = ""
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
        var request = new ContractSourceCodeRequest
        {
            Address = "0xBB9bc244D798123fDe783fCc1C72d3Bb8C189413"
        };

        // Act
        var result = await client.GetContractSourceCodeAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.Single(result.Result);
        Assert.Equal("Test", result.Result[0].ContractName);
        Assert.Equal("v0.8.0", result.Result[0].CompilerVersion);
    }

    [Fact]
    public async Task GetContractCreationAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<ContractCreationResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<ContractCreationResponse>
            {
                new()
                {
                    ContractAddress = "0xBB9bc244D798123fDe783fCc1C72d3Bb8C189413",
                    ContractCreator = "0x123456789abcdef",
                    TxHash = "0x987654321fedcba",
                    BlockNumber = "12345",
                    Timestamp = "1634567890",
                    ContractFactory = "",
                    CreationBytecode = "0x606060..."
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
        var request = new ContractCreationRequest
        {
            ContractAddresses = "0xBB9bc244D798123fDe783fCc1C72d3Bb8C189413,0x123456789abcdef"
        };

        // Act
        var result = await client.GetContractCreationAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.Single(result.Result);
        Assert.Equal("0xBB9bc244D798123fDe783fCc1C72d3Bb8C189413", result.Result[0].ContractAddress);
        Assert.Equal("0x123456789abcdef", result.Result[0].ContractCreator);
    }
} 