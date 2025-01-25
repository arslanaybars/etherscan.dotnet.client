using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Block.Request;
using EtherScan.Dotnet.Client.Models.Block.Response;

namespace EtherScan.Dotnet.Client.Tests.Block;

public class BlockTests : EtherScanClientTests
{
    private const string ApiKey = "APIKEY";
    private const int ChainId = 1;

    [Fact]
    public async Task GetBlockAndUncleRewardsByBlockNoAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<BlockRewardResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new BlockRewardResponse
            {
                BlockNumber = "2165403",
                TimeStamp = "1472533979",
                BlockMiner = "0x13a06d3dfe21e0db5c016c03ea7d2509f7f8d1e3",
                BlockReward = "5314181600000000000",
                Uncles = new List<UncleBlock>
                {
                    new()
                    {
                        Miner = "0xbcdfc35b86bedf72f0cda046a3c16829a2ef41d1",
                        UnclePosition = "0",
                        BlockReward = "3750000000000000000"
                    }
                },
                UncleInclusionReward = "312500000000000000"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new BlockRewardRequest { BlockNo = 2165403 };

        // Act
        var result = await client.GetBlockAndUncleRewardsByBlockNoAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("2165403", result.Result.BlockNumber);
        Assert.Equal("0x13a06d3dfe21e0db5c016c03ea7d2509f7f8d1e3", result.Result.BlockMiner);
        Assert.Equal("5314181600000000000", result.Result.BlockReward);
        Assert.Single(result.Result.Uncles);
        Assert.Equal("0xbcdfc35b86bedf72f0cda046a3c16829a2ef41d1", result.Result.Uncles[0].Miner);
    }

    [Fact]
    public async Task GetBlockTransactionCountByBlockNoAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<BlockTransactionCountResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new BlockTransactionCountResponse
            {
                Block = "2165403",
                TxsCount = "4",
                InternalTxsCount = "0",
                Erc20TxsCount = "0",
                Erc721TxsCount = "0",
                Erc1155TxsCount = "0"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new BlockTransactionCountRequest { BlockNo = 2165403 };

        // Act
        var result = await client.GetBlockTransactionCountByBlockNoAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("2165403", result.Result.Block);
        Assert.Equal("4", result.Result.TxsCount);
        Assert.Equal("0", result.Result.InternalTxsCount);
    }

    [Fact]
    public async Task GetEstimatedBlockCountdownTimeByBlockNoAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<BlockCountdownResponse>
        {
            Status = "1",
            Message = "OK",
            Result = new BlockCountdownResponse
            {
                CurrentBlock = "12715477",
                CountdownBlock = "16701588",
                RemainingBlock = "3986111",
                EstimateTimeInSec = "52616680.2"
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new BlockCountdownRequest { BlockNo = 16701588 };

        // Act
        var result = await client.GetEstimatedBlockCountdownTimeByBlockNoAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("12715477", result.Result.CurrentBlock);
        Assert.Equal("16701588", result.Result.CountdownBlock);
        Assert.Equal("3986111", result.Result.RemainingBlock);
        Assert.Equal("52616680.2", result.Result.EstimateTimeInSec);
    }

    [Fact]
    public async Task GetBlockNumberByTimestampAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<string>
        {
            Status = "1",
            Message = "OK",
            Result = "12712551"
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new BlockNumberByTimestampRequest 
        { 
            Timestamp = 1578638524,
            Closest = "before"
        };

        // Act
        var result = await client.GetBlockNumberByTimestampAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Equal("12712551", result.Result);
    }

    [Fact]
    public async Task GetDailyAverageBlockSizeAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<DailyAverageBlockSizeResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<DailyAverageBlockSizeResponse>
            {
                new()
                {
                    UtcDate = "2019-02-01",
                    UnixTimeStamp = "1548979200",
                    BlockSizeBytes = "20373"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new DailyStatsRequest 
        { 
            StartDate = "2019-02-01",
            EndDate = "2019-02-28",
            Sort = "asc"
        };

        // Act
        var result = await client.GetDailyAverageBlockSizeAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal("2019-02-01", result.Result[0].UtcDate);
        Assert.Equal("20373", result.Result[0].BlockSizeBytes);
    }

    [Fact]
    public async Task GetDailyBlockCountAndRewardsAsync_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new Response<List<DailyBlockCountAndRewardsResponse>>
        {
            Status = "1",
            Message = "OK",
            Result = new List<DailyBlockCountAndRewardsResponse>
            {
                new()
                {
                    UtcDate = "2019-02-01",
                    UnixTimeStamp = "1548979200",
                    BlockCount = "4848",
                    BlockRewardsEth = "14929.464690870590355682"
                }
            }
        };

        var client = new EtherScanClient(ApiKey, ChainId, CreateMockHttpClient(expectedResponse));
        var request = new DailyStatsRequest 
        { 
            StartDate = "2019-02-01",
            EndDate = "2019-02-28",
            Sort = "asc"
        };

        // Act
        var result = await client.GetDailyBlockCountAndRewardsAsync(request);

        // Assert
        Assert.Equal("1", result.Status);
        Assert.Equal("OK", result.Message);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Result);
        Assert.Equal("2019-02-01", result.Result[0].UtcDate);
        Assert.Equal("4848", result.Result[0].BlockCount);
        Assert.Equal("14929.464690870590355682", result.Result[0].BlockRewardsEth);
    }
} 