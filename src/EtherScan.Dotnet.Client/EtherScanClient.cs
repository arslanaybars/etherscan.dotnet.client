using EtherScan.Dotnet.Client.Models.Account.Request;
using EtherScan.Dotnet.Client.Models.Account.Response;
using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Block.Request;
using EtherScan.Dotnet.Client.Models.Block.Response;
using EtherScan.Dotnet.Client.Models.ChainLogs.Request;
using EtherScan.Dotnet.Client.Models.ChainLogs.Response;
using EtherScan.Dotnet.Client.Models.Contract.Request;
using EtherScan.Dotnet.Client.Models.Contract.Response;
using EtherScan.Dotnet.Client.Models.Enumerations;
using EtherScan.Dotnet.Client.Models.Transaction.Request;
using EtherScan.Dotnet.Client.Models.Transaction.Response;
using EtherScan.Dotnet.Client.Models.Token.Request;
using EtherScan.Dotnet.Client.Models.Token.Response;
using EtherScan.Dotnet.Client.Models.GasTracker.Response;
using EtherScan.Dotnet.Client.Models.GasTracker.Request;
using EtherScan.Dotnet.Client.Models.Usage.Response;
using EtherScan.Dotnet.Client.Models.Usage.Request;

namespace EtherScan.Dotnet.Client;

public class EtherScanClient : IEtherScanClient
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly int _chainId;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// If chainId is available in the ChainNetwork enum, use this constructor
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="chainId"></param>
    /// <param name="httpClient"></param>
    public EtherScanClient(string apiKey, ChainNetwork chainId, HttpClient httpClient)
    {
        _apiKey = apiKey;
        _chainId = (int)chainId;
        _baseUrl = "https://api.etherscan.io/v2/";
        _httpClient = httpClient;
    }
    
    /// <summary>
    /// If chainId is not available in the ChainNetwork enum, use this constructor
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="chainId"></param>
    /// <param name="httpClient"></param>
    public EtherScanClient(string apiKey, int chainId, HttpClient httpClient)
    {
        _apiKey = apiKey;
        _chainId = chainId;
        _baseUrl = "https://api.etherscan.io/v2/";
        _httpClient = httpClient;
    }

    #region [ Account Module ]

     public async Task<Response<AccountBalanceResponse>> GetAccountBalanceAsync(AccountBalanceRequest address,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "balance", address);
        var result = await ExecuteHttpRequestAsync<AccountBalanceRequest, decimal>(request, cancellationToken);

        return new Response<AccountBalanceResponse>
        {
            Status = result.Status,
            Message = result.Message,
            Result = new AccountBalanceResponse
            {
                Balance = result.Result
            }
        };
    }

    public async Task<Response<List<AccountBalanceResponse>>> GetMultipleAccountBalanceAsync(
        AccountBalanceRequest address,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "balancemulti", address);
        return await ExecuteHttpRequestAsync<AccountBalanceRequest, List<AccountBalanceResponse>>(request,
            cancellationToken);
    }

    public async Task<Response<List<TransactionResponse>>> GetTransactionsByAddressAsync(
        TransactionRequest transactionRequest,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "txlist", transactionRequest);
        return await ExecuteHttpRequestAsync<TransactionRequest, List<TransactionResponse>>(request, cancellationToken);
    }

    public async Task<Response<List<TransactionResponse>>> GetInternalTransactionsByAddressAsync(TransactionRequest transactionRequest,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "txlistinternal", transactionRequest);
        return await ExecuteHttpRequestAsync<TransactionRequest, List<TransactionResponse>>(request, cancellationToken);
    }

    public async Task<Response<List<TransactionResponse>>> GetInternalTransactionsByTransactionHashAsync(TxnTransactionRequest txnTransactionRequest,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "txlistinternal", txnTransactionRequest);
        return await ExecuteHttpRequestAsync<TxnTransactionRequest, List<TransactionResponse>>(request, cancellationToken);
    }
    
    public async Task<Response<List<TransactionResponse>>> GetInternalTransactionsByBlockRangeAsync(
        BlockRangeRequest blockRangeRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "txlistinternal", blockRangeRequest);
        return await ExecuteHttpRequestAsync<BlockRangeRequest, List<TransactionResponse>>(request, cancellationToken);
    }
    
    public async Task<Response<List<Erc20TokenTransferEventResponse>>> GetErc20TokenTransferEventsByAddressAsync(
        Erc20TokenTransferEventRequest erc20TokenTransferEventRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "tokentx", erc20TokenTransferEventRequest);
        return await ExecuteHttpRequestAsync<Erc20TokenTransferEventRequest, List<Erc20TokenTransferEventResponse>>(request, cancellationToken);
    }
    
    public async Task<Response<List<Erc721TokenTransferEventResponse>>> GetErc721TokenTransferEventsByAddressAsync(
        Erc721TokenTransferEventRequest erc721TokenTransferEventRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "tokennfttx", erc721TokenTransferEventRequest);
        return await ExecuteHttpRequestAsync<Erc721TokenTransferEventRequest, List<Erc721TokenTransferEventResponse>>(request, cancellationToken);
    }
    
    public async Task<Response<List<Erc1155TokenTransferEventResponse>>> GetErc1155TokenTransferEventsByAddressAsync(
        Erc1155TokenTransferEventRequest erc1155TokenTransferEventRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "token1155tx", erc1155TokenTransferEventRequest);
        return await ExecuteHttpRequestAsync<Erc1155TokenTransferEventRequest, List<Erc1155TokenTransferEventResponse>>(request, cancellationToken);
    }

    public async Task<Response<List<MinedBlockResponse>>> GetBlocksValidatedByAddressAsync(
        MinedBlockRequest minedBlockRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "getminedblocks", minedBlockRequest);
        return await ExecuteHttpRequestAsync<MinedBlockRequest, List<MinedBlockResponse>>(request, cancellationToken);
    }
    
    public async Task<Response<List<BeaconChainWithdrawalResponse>>> GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync(
        BeaconChainWithdrawalRequest beaconChainWithdrawalRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "txsBeaconWithdrawal", beaconChainWithdrawalRequest);
        return await ExecuteHttpRequestAsync<BeaconChainWithdrawalRequest, List<BeaconChainWithdrawalResponse>>(request, cancellationToken);
    }
    
    public async Task<Response<HistoricalEtherBalanceResponse>> GetHistoricalEtherBalanceByBlockNoAsync(
        HistoricalEtherBalanceRequest historicalEtherBalanceRequest, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("account", "balancehistory", historicalEtherBalanceRequest);
        return await ExecuteHttpRequestAsync<HistoricalEtherBalanceRequest, HistoricalEtherBalanceResponse>(request, cancellationToken);
    }

    #endregion

    #region [ Contract Module ]
    
    public async Task<Response<string>> GetContractAbiAsync(ContractAbiRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("contract", "getabi", request);
        return await ExecuteHttpRequestAsync<ContractAbiRequest, string>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<ContractSourceCodeResponse>>> GetContractSourceCodeAsync(ContractSourceCodeRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("contract", "getsourcecode", request);
        return await ExecuteHttpRequestAsync<ContractSourceCodeRequest, List<ContractSourceCodeResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<ContractCreationResponse>>> GetContractCreationAsync(ContractCreationRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("contract", "getcontractcreation", request);
        return await ExecuteHttpRequestAsync<ContractCreationRequest, List<ContractCreationResponse>>(httpRequest, cancellationToken);
    }

    #endregion

    #region [ Transaction Module ]

    public async Task<Response<TransactionStatusResponse>> GetContractExecutionStatusAsync(
        TransactionStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("transaction", "getstatus", request);
        return await ExecuteHttpRequestAsync<TransactionStatusRequest, TransactionStatusResponse>(
            httpRequest, 
            cancellationToken);
    }

    public async Task<Response<TransactionReceiptStatusResponse>> GetTransactionReceiptStatusAsync(
        TransactionStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("transaction", "gettxreceiptstatus", request);
        return await ExecuteHttpRequestAsync<TransactionStatusRequest, TransactionReceiptStatusResponse>(
            httpRequest, 
            cancellationToken);
    }

    #endregion

    #region [ Block Module ]

    public async Task<Response<BlockRewardResponse>> GetBlockAndUncleRewardsByBlockNoAsync(
        BlockRewardRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("block", "getblockreward", request);
        return await ExecuteHttpRequestAsync<BlockRewardRequest, BlockRewardResponse>(httpRequest, cancellationToken);
    }

    public async Task<Response<BlockTransactionCountResponse>> GetBlockTransactionCountByBlockNoAsync(
        BlockTransactionCountRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("block", "getblocktxnscount", request);
        return await ExecuteHttpRequestAsync<BlockTransactionCountRequest, BlockTransactionCountResponse>(httpRequest, cancellationToken);
    }

    public async Task<Response<BlockCountdownResponse>> GetEstimatedBlockCountdownTimeByBlockNoAsync(
        BlockCountdownRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("block", "getblockcountdown", request);
        return await ExecuteHttpRequestAsync<BlockCountdownRequest, BlockCountdownResponse>(httpRequest, cancellationToken);
    }

    public async Task<Response<string>> GetBlockNumberByTimestampAsync(
        BlockNumberByTimestampRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("block", "getblocknobytime", request);
        return await ExecuteHttpRequestAsync<BlockNumberByTimestampRequest, string>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyAverageBlockSizeResponse>>> GetDailyAverageBlockSizeAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyavgblocksize", request);
        return await ExecuteHttpRequestAsync<DailyStatsRequest, List<DailyAverageBlockSizeResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyBlockCountAndRewardsResponse>>> GetDailyBlockCountAndRewardsAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyblkcount", request);
        return await ExecuteHttpRequestAsync<DailyStatsRequest, List<DailyBlockCountAndRewardsResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyBlockRewardsResponse>>> GetDailyBlockRewardsAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyblockrewards", request);
        return await ExecuteHttpRequestAsync<DailyStatsRequest, List<DailyBlockRewardsResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyAverageBlockTimeResponse>>> GetDailyAverageBlockTimeAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyavgblocktime", request);
        return await ExecuteHttpRequestAsync<DailyStatsRequest, List<DailyAverageBlockTimeResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyUncleBlockCountAndRewardsResponse>>> GetDailyUncleBlockCountAndRewardsAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyuncleblkcount", request);
        return await ExecuteHttpRequestAsync<DailyStatsRequest, List<DailyUncleBlockCountAndRewardsResponse>>(httpRequest, cancellationToken);
    }

    #endregion

    #region [ Logs Module ]
    
    public async Task<Response<List<LogResponse>>> GetLogsAsync(
        LogRequest request, 
        CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("logs", "getLogs", request);
        return await ExecuteHttpRequestAsync<LogRequest, List<LogResponse>>(httpRequest, cancellationToken);
    }

    #endregion

    #region [ Token Module ]

    public async Task<Response<string>> GetErc20TokenSupplyAsync(TokenSupplyRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "tokensupply", request);
        return await ExecuteHttpRequestAsync<TokenSupplyRequest, string>(httpRequest, cancellationToken);
    }

    public async Task<Response<string>> GetErc20TokenBalanceAsync(TokenBalanceRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("account", "tokenbalance", request);
        return await ExecuteHttpRequestAsync<TokenBalanceRequest, string>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<TokenHolderResponse>>> GetTokenHolderListAsync(TokenHolderListRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("token", "tokenholderlist", request);
        return await ExecuteHttpRequestAsync<TokenHolderListRequest, List<TokenHolderResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<TokenInfoResponse>>> GetTokenInfoAsync(TokenSupplyRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("token", "tokeninfo", request);
        return await ExecuteHttpRequestAsync<TokenSupplyRequest, List<TokenInfoResponse>>(httpRequest, cancellationToken);
    }

    #endregion

    #region [ Gas Tracker Module ]

    public async Task<Response<GasEstimateResponse>> GetEstimationOfConfirmationTimeAsync(
        GasEstimateRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("gastracker", "gasestimate", request);
        return await ExecuteHttpRequestAsync<GasEstimateRequest, GasEstimateResponse>(httpRequest, cancellationToken);
    }

    public async Task<Response<GasOracleResponse>> GetGasOracleAsync(CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("gastracker", "gasoracle", new GasOracleRequest());
        return await ExecuteHttpRequestAsync<GasOracleRequest, GasOracleResponse>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyGasLimitResponse>>> GetDailyAverageGasLimitAsync(
        DailyGasStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyavggaslimit", request);
        return await ExecuteHttpRequestAsync<DailyGasStatsRequest, List<DailyGasLimitResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyGasUsedResponse>>> GetDailyTotalGasUsedAsync(
        DailyGasStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailygasused", request);
        return await ExecuteHttpRequestAsync<DailyGasStatsRequest, List<DailyGasUsedResponse>>(httpRequest, cancellationToken);
    }

    public async Task<Response<List<DailyAverageGasPriceResponse>>> GetDailyAverageGasPriceAsync(
        DailyGasStatsRequest request, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateRequest("stats", "dailyavggasprice", request);
        return await ExecuteHttpRequestAsync<DailyGasStatsRequest, List<DailyAverageGasPriceResponse>>(httpRequest, cancellationToken);
    }

    #endregion

    #region [ Usage Module ]

    public async Task<Response<ApiLimitResponse>> GetApiCreditUsageAsync(CancellationToken cancellationToken = default)
    {
        var request = CreateRequest("getapilimit", "getapilimit", new ApiLimitRequest());
        return await ExecuteHttpRequestAsync<ApiLimitRequest, ApiLimitResponse>(request, cancellationToken);
    }

    public async Task<ChainListResponse> GetSupportedChainsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/chainlist", cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ChainListResponse>(
                cancellationToken: cancellationToken);

            return result ?? new ChainListResponse 
            { 
                TotalCount = 0,
                Result = new List<ChainInfoResponse>() 
            };
        }
        catch (Exception ex)
        {
            return new ChainListResponse
            {
                TotalCount = 0,
                Result = new List<ChainInfoResponse>(),
            };
        }
    }

    #endregion
   
    private async Task<Response<TResponse>> ExecuteHttpRequestAsync<TRequest, TResponse>(
        Request<TRequest> request,
        CancellationToken cancellationToken) where TRequest : IUrlParameterizable
    {
        try
        {
            var requestUrl = BuildRequestUrl(request);
            using var response = await _httpClient.GetAsync(requestUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Response<TResponse>>(
                cancellationToken: cancellationToken);

            return result ?? new Response<TResponse> { Status = "0", Message = "Failed to deserialize response" };
        }
        catch (HttpRequestException ex)
        {
            return new Response<TResponse>
            {
                Status = "0",
                Message = $"HTTP request failed: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new Response<TResponse>
            {
                Status = "0",
                Message = $"Unexpected error: {ex.Message}"
            };
        }
    }

    private string BuildRequestUrl<T>(Request<T> request) where T : IUrlParameterizable
    {
        var urlBuilder = new StringBuilder(_baseUrl)
            .Append("/api")
            .Append("?chainid=").Append(_chainId)
            .Append("&module=").Append(request.Module)
            .Append("&action=").Append(request.Action)
            .Append("&apikey=").Append(_apiKey);

        request.Parameters.AppendUrlParameters(urlBuilder);

        return urlBuilder.ToString();
    }

    private Request<T> CreateRequest<T>(string module, string action, T parameters) where T : class
    {
        return new Request<T>
        {
            Module = module,
            Action = action,
            ApiKey = _apiKey,
            Parameters = parameters
        };
    }
}