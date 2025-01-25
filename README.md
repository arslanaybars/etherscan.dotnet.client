# EtherScan .NET Client

A comprehensive .NET client library for interacting with the EtherScan API.

For detailed endpoint information, see the official [EtherScan V2 API Documentation](https://docs.etherscan.io/etherscan-v2).

## Contents
- [Installation](#installation)
- [Features](#features)
- [Quick Start](#quick-start)
- [Usage Examples](#usage-examples)
- [Supported Chains](#supported-chains)
- [API Reference](#api-reference)
- [Error Handling](#error-handling)
- [Contributing](#contributing)
- [License](#license)

## Installation

```bash
dotnet add package EtherScan.Dotnet.Client
```

## Features

- Full coverage of EtherScan API endpoints
- Strongly typed request/response models
- Async/await support
- Comprehensive error handling
- Multi-chain support
- Built-in rate limiting handling
- Automatic Wei to Ether conversion

## Quick Start

```csharp
using EtherScan.Dotnet.Client;
using EtherScan.Dotnet.Client.Models.Account.Request;
using EtherScan.Dotnet.Client.Models.Enumerations;

var client = new EtherScanClient("YOUR_API_KEY", ChainNetwork.EthereumMainnet, new HttpClient());

// Get account balance
var balance = await client.GetAccountBalanceAsync(new AccountBalanceRequest 
{ 
    Address = "0x742d35Cc6634C0532925a3b844Bc454e4438f44e" 
});

Console.WriteLine($"Balance: {balance.Result?.BalanceInEther} ETH");
```

## Usage Examples

### Account Module
```csharp
// Get multiple account balances
var balances = await client.GetMultipleAccountBalanceAsync(
    new AccountBalanceRequest 
    { 
        Address = "0x742d...44e,0xdac1...ec7" 
    });

// Get transactions
var txs = await client.GetTransactionsByAddressAsync(new TransactionRequest
{
    Address = "0x742d35Cc6634C0532925a3b844Bc454e4438f44e",
    StartBlock = 0,
    EndBlock = 99999999,
    Sort = "desc"
});
```

### Contract Module
```csharp
// Get contract ABI
var abi = await client.GetContractAbiAsync(new ContractAbiRequest 
{ 
    Address = "0xdac17f958d2ee523a2206206994597c13d831ec7" 
});

// Get contract source code
var source = await client.GetContractSourceCodeAsync(new ContractSourceCodeRequest 
{ 
    Address = "0xdac17f958d2ee523a2206206994597c13d831ec7" 
});
```

## Supported Chains

- Ethereum Mainnet
- Sepolia Testnet
- Holesky Testnet
- BNB Chain
- Polygon
- Arbitrum
- Optimism
- Avalanche
- [Full list of supported networks](src/EtherScan.Dotnet.Client/Models/Enumerations/ChainNetwork.cs)

## API Reference

See [IEtherScanClient.cs](src/EtherScan.Dotnet.Client/IEtherScanClient.cs) for complete API documentation.

## Error Handling

The client provides detailed error information through the Response<T> class:

```csharp
public class Response<T>
{
    public string Status { get; set; }
    public bool IsSuccess => Status == "1" && Message == "OK";
    public string Message { get; set; }
    public T? Result { get; set; }
}
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.