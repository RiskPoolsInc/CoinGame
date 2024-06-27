using System.Net;
using App.Interfaces.Services;
using App.Services.BlockChainExplorer.Options;
using Newtonsoft.Json;

namespace App.Services.BlockChainExplorer;

public class BlockChainExplorer : IBlockChainExplorer
{
    private readonly BlockChainExplorerConfig _chainExplorerConfig;

    public BlockChainExplorer(BlockChainExplorerConfig chainExplorerConfig)
    {
        _chainExplorerConfig = chainExplorerConfig;
    }

    public async Task<bool> CheckExistHash(string hash, CancellationToken cancellationToken)
    {
        var handler = new HttpClientHandler();

        using var client = new HttpClient(handler);
        var result = await SendGetRequest(client, "transaction/" + hash, cancellationToken);

        return result.IsSuccessStatusCode;
        // var tx = JsonConvert.DeserializeObject(result.Content, typeof(Transaction)) as Transaction;
    }

    private async Task<(string Content, HttpStatusCode Code, bool IsSuccessStatusCode)> SendGetRequest(
        HttpClient client,
        string endPoint, CancellationToken cancellationToken = default)
    {
        var url = new Uri(_chainExplorerConfig.Host + endPoint);
        var response = await client.GetAsync(url, cancellationToken);
        var resultCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return (content, resultCode, response.IsSuccessStatusCode);
    }

    public Task<object> GetHashData(string hash, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private class Transaction
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("block")] public string Block { get; set; }

        [JsonProperty("tx")] public Tx Tx { get; set; }
    }

    private class Tx
    {
        [JsonProperty("claimProofs")] public object[] ClaimProofs { get; set; }
        [JsonProperty("payload")] public TxPayload Payload { get; set; }
    }

    private class TxPayload
    {
        public int version { get; set; }
        public int conciliumId { get; set; }
        public TxIns[] ins { get; set; }
        public TxOuts[] outs { get; set; }
    }

    private class TxIns
    {
        public string txHash { get; set; }
        public int nTxOutput { get; set; }
    }

    private class ContractCode
    {
        public string method { get; set; }
        public object[] arrArguments { get; set; }
    }

    private class IntTx
    {
        public string intTxHash { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    private class TokenTransfer
    {
        public string transactionHash { get; set; }
        public string symbol { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string quantity { get; set; }
        public long timestamp { get; set; }
    }

    private class TxOuts
    {
        public string receiverAddr { get; set; }
        public string addrChangeReceiver { get; set; }

        public decimal amount { get; set; }
        public ContractCode contractCode { get; set; }
        public int nTx { get; set; }
        public IntTx[] intTx { get; set; }
        public TokenTransfer tokenTransfer { get; set; }
    }
}