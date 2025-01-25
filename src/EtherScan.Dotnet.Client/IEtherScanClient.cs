using EtherScan.Dotnet.Client.Models.Account.Request;
using EtherScan.Dotnet.Client.Models.Account.Response;
using EtherScan.Dotnet.Client.Models.Base;
using EtherScan.Dotnet.Client.Models.Contract.Request;
using EtherScan.Dotnet.Client.Models.Contract.Response;
using EtherScan.Dotnet.Client.Models.Transaction.Request;
using EtherScan.Dotnet.Client.Models.Transaction.Response;
using EtherScan.Dotnet.Client.Models.Block.Request;
using EtherScan.Dotnet.Client.Models.Block.Response;
using EtherScan.Dotnet.Client.Models.ChainLogs.Request;
using EtherScan.Dotnet.Client.Models.ChainLogs.Response;
using EtherScan.Dotnet.Client.Models.Token.Request;
using EtherScan.Dotnet.Client.Models.Token.Response;
using EtherScan.Dotnet.Client.Models.GasTracker.Response;
using EtherScan.Dotnet.Client.Models.GasTracker.Request;
using EtherScan.Dotnet.Client.Models.Usage.Response;

namespace EtherScan.Dotnet.Client;

public interface IEtherScanClient
{
    #region [ Account Module ]

    /// <summary>
    /// Returns the Ether balance of a given address.
    /// </summary>
    /// <param name="address"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<AccountBalanceResponse>> GetAccountBalanceAsync(AccountBalanceRequest address,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the balance of the accounts from a list of addresses.
    /// </summary>
    /// <param name="addresses"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<AccountBalanceResponse>>> GetMultipleAccountBalanceAsync(AccountBalanceRequest addresses,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the list of transactions performed by an address, with optional pagination.
    /// </summary>
    /// <param name="transactionRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<TransactionResponse>>> GetTransactionsByAddressAsync(TransactionRequest transactionRequest,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the list of internal transactions performed by an address, with optional pagination.
    /// </summary>
    /// <param name="transactionRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<TransactionResponse>>> GetInternalTransactionsByAddressAsync(
        TransactionRequest transactionRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the list of internal transactions performed within a transaction.
    /// </summary>
    /// <param name="txnTransactionRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<TransactionResponse>>> GetInternalTransactionsByTransactionHashAsync(
        TxnTransactionRequest txnTransactionRequest, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns the list of internal transactions performed within a block range, with optional pagination.
    /// </summary>
    /// <param name="blockRangeRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<TransactionResponse>>> GetInternalTransactionsByBlockRangeAsync(
        BlockRangeRequest blockRangeRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the list of ERC-20 tokens transferred by an address, with optional filtering by token contract.
    /// </summary>
    /// <param name="erc20TokenTransferEventRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<Erc20TokenTransferEventResponse>>> GetErc20TokenTransferEventsByAddressAsync(
        Erc20TokenTransferEventRequest erc20TokenTransferEventRequest, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns the list of ERC-721 ( NFT ) tokens transferred by an address, with optional filtering by token contract.
    /// </summary>
    /// <param name="erc721TokenTransferEventRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<Erc721TokenTransferEventResponse>>> GetErc721TokenTransferEventsByAddressAsync(
        Erc721TokenTransferEventRequest erc721TokenTransferEventRequest, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns the list of ERC-1155 ( Multi Token Standard ) tokens transferred by an address, with optional filtering by token contract.
    /// </summary>
    /// <param name="erc1155TokenTransferEventRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<Erc1155TokenTransferEventResponse>>> GetErc1155TokenTransferEventsByAddressAsync(
        Erc1155TokenTransferEventRequest erc1155TokenTransferEventRequest, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns the list of blocks validated by an address.
    /// </summary>
    /// <param name="minedBlockRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<MinedBlockResponse>>> GetBlocksValidatedByAddressAsync(
        MinedBlockRequest minedBlockRequest, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns the beacon chain withdrawals made to an address.
    /// </summary>
    /// <param name="beaconChainWithdrawalRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<List<BeaconChainWithdrawalResponse>>> GetBeaconChainWithdrawalsByAddressAndBlockRangeAsync(
        BeaconChainWithdrawalRequest beaconChainWithdrawalRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pro Version
    /// Returns the balance of an address at a certain block height.
    /// </summary>
    /// <param name="historicalEtherBalanceRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<HistoricalEtherBalanceResponse>> GetHistoricalEtherBalanceByBlockNoAsync(
        HistoricalEtherBalanceRequest historicalEtherBalanceRequest, CancellationToken cancellationToken = default);

    
    #endregion

    #region [ Contract Module ]
    
    /// <summary>
    /// Returns the Contract Application Binary Interface (ABI) of a verified smart contract
    /// </summary>
    Task<Response<string>> GetContractAbiAsync(ContractAbiRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the Solidity source code of a verified smart contract
    /// </summary>
    Task<Response<List<ContractSourceCodeResponse>>> GetContractSourceCodeAsync(ContractSourceCodeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a contract's deployer address and transaction hash it was created, up to 5 at a time
    /// </summary>
    Task<Response<List<ContractCreationResponse>>> GetContractCreationAsync(ContractCreationRequest request, CancellationToken cancellationToken = default);

    #endregion
    
    #region [ Transaction Module ]

    /// <summary>
    /// Returns the status code of a contract execution
    /// </summary>
    Task<Response<TransactionStatusResponse>> GetContractExecutionStatusAsync(
        TransactionStatusRequest request, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the status code of a transaction execution
    /// </summary>
    Task<Response<TransactionReceiptStatusResponse>> GetTransactionReceiptStatusAsync(
        TransactionStatusRequest request, 
        CancellationToken cancellationToken = default);

    #endregion

    #region [ Block Module ]

    /// <summary>
    /// Returns the block reward and 'Uncle' block rewards.
    /// </summary>
    Task<Response<BlockRewardResponse>> GetBlockAndUncleRewardsByBlockNoAsync(
        BlockRewardRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the number of transactions in a specified block.
    /// </summary>
    Task<Response<BlockTransactionCountResponse>> GetBlockTransactionCountByBlockNoAsync(
        BlockTransactionCountRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the estimated time remaining, in seconds, until a certain block is mined.
    /// </summary>
    Task<Response<BlockCountdownResponse>> GetEstimatedBlockCountdownTimeByBlockNoAsync(
        BlockCountdownRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the block number that was mined at a certain timestamp.
    /// </summary>
    Task<Response<string>> GetBlockNumberByTimestampAsync(
        BlockNumberByTimestampRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the daily average block size within a date range.
    /// </summary>
    Task<Response<List<DailyAverageBlockSizeResponse>>> GetDailyAverageBlockSizeAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the number of blocks mined daily and the amount of block rewards.
    /// </summary>
    Task<Response<List<DailyBlockCountAndRewardsResponse>>> GetDailyBlockCountAndRewardsAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the amount of block rewards distributed to miners daily.
    /// </summary>
    Task<Response<List<DailyBlockRewardsResponse>>> GetDailyBlockRewardsAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the daily average of time needed for a block to be successfully mined.
    /// </summary>
    Task<Response<List<DailyAverageBlockTimeResponse>>> GetDailyAverageBlockTimeAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the number of 'Uncle' blocks mined daily and the amount of 'Uncle' block rewards.
    /// </summary>
    Task<Response<List<DailyUncleBlockCountAndRewardsResponse>>> GetDailyUncleBlockCountAndRewardsAsync(
        DailyStatsRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region [ Logs Module ]
    
    /// <summary>
    /// Returns the event logs from an address, with optional filtering by block range and topics.
    /// </summary>
    Task<Response<List<LogResponse>>> GetLogsAsync(LogRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region [ Token Module ]

    /// <summary>
    /// Returns the current amount of an ERC-20 token in circulation
    /// </summary>
    Task<Response<string>> GetErc20TokenSupplyAsync(TokenSupplyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the current balance of an ERC-20 token of an address
    /// </summary>
    Task<Response<string>> GetErc20TokenBalanceAsync(TokenBalanceRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the current ERC20 token holders and number of tokens held
    /// </summary>
    Task<Response<List<TokenHolderResponse>>> GetTokenHolderListAsync(TokenHolderListRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns project information and social media links of an ERC20/ERC721/ERC1155 token
    /// </summary>
    Task<Response<List<TokenInfoResponse>>> GetTokenInfoAsync(TokenSupplyRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region [ Gas Tracker Module ]

    /// <summary>
    /// Returns the estimated time, in seconds, for a transaction to be confirmed on the blockchain.
    /// </summary>
    Task<Response<GasEstimateResponse>> GetEstimationOfConfirmationTimeAsync(
        GasEstimateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the current Safe, Proposed and Fast gas prices.
    /// </summary>
    Task<Response<GasOracleResponse>> GetGasOracleAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the historical daily average gas limit of the Ethereum network.
    /// </summary>
    Task<Response<List<DailyGasLimitResponse>>> GetDailyAverageGasLimitAsync(
        DailyGasStatsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the total amount of gas used daily for transactions on the Ethereum network.
    /// </summary>
    Task<Response<List<DailyGasUsedResponse>>> GetDailyTotalGasUsedAsync(
        DailyGasStatsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the daily average gas price used on the Ethereum network.
    /// </summary>
    Task<Response<List<DailyAverageGasPriceResponse>>> GetDailyAverageGasPriceAsync(
        DailyGasStatsRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region [ Usage Module ]

    /// <summary>
    /// Returns the amount of API credits available, and reset countdown.
    /// </summary>
    Task<Response<ApiLimitResponse>> GetApiCreditUsageAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of supported Etherscan explorer APIs, with web explorer links.
    /// </summary>
    Task<ChainListResponse> GetSupportedChainsAsync(CancellationToken cancellationToken = default);

    #endregion
}