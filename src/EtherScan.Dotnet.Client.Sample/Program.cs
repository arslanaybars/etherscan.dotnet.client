using EtherScan.Dotnet.Client.Models.Account.Request;
using EtherScan.Dotnet.Client.Models.Block.Request;
using EtherScan.Dotnet.Client.Models.ChainLogs.Request;
using EtherScan.Dotnet.Client.Models.Contract.Request;
using EtherScan.Dotnet.Client.Models.Enumerations;
using EtherScan.Dotnet.Client.Models.GasTracker.Request;
using EtherScan.Dotnet.Client.Models.Token.Request;
using EtherScan.Dotnet.Client.Models.Transaction.Request;

const string apiKey = "YOUR_API_KEY";
const string testAddress = "0x742d35Cc6634C0532925a3b844Bc454e4438f44e";
const string testContractAddress = "0xdac17f958d2ee523a2206206994597c13d831ec7"; // USDT Contract
const string testTxHash = "0x3c9011ecd7d4c6a41191ceab46e5bcac23f5d2f75dddf90d28eaa7dcd9d2e369";

using var httpClient = new HttpClient();
var client = new EtherScanClient(apiKey, ChainNetwork.EthereumMainnet, httpClient);

try
{
    await DemoAccountModule();
    await DemoContractModule();
    await DemoTransactionModule();
    await DemoBlockModule();
    await DemoLogsModule();
    await DemoTokenModule();
    await DemoGasTrackerModule();
    await DemoUsageModule();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

async Task DemoAccountModule()
{
    Console.WriteLine("\n=== Account Module Demo ===");

    // Get single account balance
    var balance = await client.GetAccountBalanceAsync(new AccountBalanceRequest { Address = testAddress });
    Console.WriteLine($"Account Balance: {balance.Result?.BalanceInEther} ETH");

    // Get multiple account balances
    var multiBalance = await client.GetMultipleAccountBalanceAsync(
        new AccountBalanceRequest { Address = $"{testAddress},{testContractAddress}" });
    Console.WriteLine("Multiple Account Balances:");
    foreach (var acc in multiBalance.Result ?? new())
    {
        Console.WriteLine($"- {acc.Account}: {acc.BalanceInEther} ETH");
    }

    // Get normal transactions
    var txs = await client.GetTransactionsByAddressAsync(new TransactionRequest
    {
        Address = testAddress,
        StartBlock = 0,
        EndBlock = 99999999,
        Page = 1,
        Offset = 10,
        Sort = "desc"
    });
    Console.WriteLine("\nRecent Transactions:");
    foreach (var tx in txs.Result?.Take(3))
    {
        Console.WriteLine($"- Hash: {tx.Hash}, Value: {tx.Value} Wei");
    }
}

async Task DemoContractModule()
{
    Console.WriteLine("\n=== Contract Module Demo ===");

    // Get contract ABI
    var abi = await client.GetContractAbiAsync(new ContractAbiRequest { Address = testContractAddress });
    Console.WriteLine($"Contract ABI Retrieved: {abi.Result?.Length > 100}");

    // Get contract source code
    var source = await client.GetContractSourceCodeAsync(new ContractSourceCodeRequest { Address = testContractAddress });
    if (source.Result?.FirstOrDefault() != null)
    {
        Console.WriteLine($"Contract Name: {source.Result[0].ContractName}");
        Console.WriteLine($"Compiler Version: {source.Result[0].CompilerVersion}");
    }
}

async Task DemoTransactionModule()
{
    Console.WriteLine("\n=== Transaction Module Demo ===");

    // Get transaction status
    var status = await client.GetContractExecutionStatusAsync(new TransactionStatusRequest { TxHash = testTxHash });
    Console.WriteLine($"Transaction Status: {status.IsSuccess}");

    // Get transaction receipt status
    var receipt = await client.GetTransactionReceiptStatusAsync(new TransactionStatusRequest { TxHash = testTxHash });
    Console.WriteLine($"Receipt Status: {receipt.Result?.Status}");
}

async Task DemoBlockModule()
{
    Console.WriteLine("\n=== Block Module Demo ===");

    // Get block rewards
    var rewards = await client.GetBlockAndUncleRewardsByBlockNoAsync(new BlockRewardRequest { BlockNo = 12345678 });
    if (rewards.Result != null)
    {
        Console.WriteLine($"Block Reward: {rewards.Result.BlockReward} Wei");
        Console.WriteLine($"Uncle Reward: {rewards.Result.UncleInclusionReward} Wei");
    }

    // Get block countdown
    var countdown = await client.GetEstimatedBlockCountdownTimeByBlockNoAsync(new BlockCountdownRequest { BlockNo = 17000000 });
    if (countdown.Result != null)
    {
        Console.WriteLine($"Estimated Time: {countdown.Result.EstimateTimeInSec} seconds");
    }
}

async Task DemoLogsModule()
{
    Console.WriteLine("\n=== Logs Module Demo ===");

    // Get logs
    var logs = await client.GetLogsAsync(new LogRequest
    {
        FromBlock = "17000000",
        ToBlock = "17000100",
        Address = testContractAddress
    });
    Console.WriteLine($"Logs Found: {logs.Result?.Count ?? 0}");
}

async Task DemoTokenModule()
{
    Console.WriteLine("\n=== Token Module Demo ===");

    // Get ERC20 token supply
    var supply = await client.GetErc20TokenSupplyAsync(new TokenSupplyRequest { ContractAddress = testContractAddress });
    Console.WriteLine($"Token Supply: {supply.Result}");

    // Get token info
    var info = await client.GetTokenInfoAsync(new TokenSupplyRequest { ContractAddress = testContractAddress });
    if (info.Result?.FirstOrDefault() != null)
    {
        Console.WriteLine($"Token Name: {info.Result[0].TokenName}");
        Console.WriteLine($"Token Symbol: {info.Result[0].Symbol}");
    }
}

async Task DemoGasTrackerModule()
{
    Console.WriteLine("\n=== Gas Tracker Module Demo ===");

    // Get gas oracle
    var oracle = await client.GetGasOracleAsync();
    if (oracle.Result != null)
    {
        Console.WriteLine($"Standard Gas Price: {oracle.Result.SafeGasPrice} Gwei");
    }

    // Get gas estimate
    var estimate = await client.GetEstimationOfConfirmationTimeAsync(new GasEstimateRequest { GasPrice = "2000000000" });
    Console.WriteLine($"Estimated Confirmation Time: {estimate.Result?.Result} seconds");
}

async Task DemoUsageModule()
{
    Console.WriteLine("\n=== Usage Module Demo ===");

    // Get API usage limits
    var usage = await client.GetApiCreditUsageAsync();
    if (usage.Result != null)
    {
        Console.WriteLine($"Credit Available Usage: {usage.Result.CreditsAvailable}");
        Console.WriteLine($"Credit Limit: {usage.Result.CreditLimit}");
    }

    // Get supported chains
    var chains = await client.GetSupportedChainsAsync();
    Console.WriteLine($"Supported Chains: {chains.TotalCount}");
    foreach (var chain in chains.Result?.Take(3))
    {
        Console.WriteLine($"- {chain.ChainName} (ID: {chain.ChainId})");
    }
}