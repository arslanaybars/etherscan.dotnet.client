namespace EtherScan.Dotnet.Client.Models.Block.Response;

public class BlockTransactionCountResponse
{
    [JsonPropertyName("block")]
    public required string Block { get; set; }

    [JsonPropertyName("txsCount")]
    public required string TxsCount { get; set; }

    [JsonPropertyName("internalTxsCount")]
    public required string InternalTxsCount { get; set; }

    [JsonPropertyName("erc20TxsCount")]
    public required string Erc20TxsCount { get; set; }

    [JsonPropertyName("erc721TxsCount")]
    public required string Erc721TxsCount { get; set; }

    [JsonPropertyName("erc1155TxsCount")]
    public required string Erc1155TxsCount { get; set; }
} 