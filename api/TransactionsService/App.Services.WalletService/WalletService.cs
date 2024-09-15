using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;

using App.Core.ViewModels.External;
using App.Services.Telegram.Options;
using App.Services.WalletService.Models;

using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace App.Services.WalletService;

public class WalletService : IWalletService {
    private readonly SystemSettingsOptions _systemSettingsOptions;
    private readonly WalletServiceOptions _walletServiceOptions;
    private readonly string _privateKey;

    public WalletService(SystemSettingsOptions systemSettingsOptions,
                         WalletServiceOptions  walletServiceOptions) {
        _systemSettingsOptions = systemSettingsOptions;
        _walletServiceOptions = walletServiceOptions;
        _privateKey = _walletServiceOptions.PrivateKey;
    }

    #region Endpoint Paths

    private string BasePath => $"{_walletServiceOptions.Host}";

    private WalletServiceEnpoint ServiceEndpoint(WalletServiceEnpointTypes typeId) =>
        _walletServiceOptions.Endpoints.Single(a => a.Type == (int)typeId);

    private string GetPath(WalletServiceEnpointTypes type) => $"{BasePath}/{ServiceEndpoint(type).Value}";

    #endregion

    #region Public Methods

    public static string EncryptPrivateKey(string privateKey, Guid key) {
        return privateKey;
    }

    public static string DecrypPrivateKey(string privateKey, Guid key) {
        return privateKey;
    }

    public async Task<GeneratedWalletView> GenerateWallet(CancellationToken cancellationToken = default) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateWallet);
        var result = await Put<GeneratedWalletView>(path, null, cancellationToken);
        return result;
    }

    public async Task<BalanceView> GetBalance(string address) {
        var path = GetPath(WalletServiceEnpointTypes.GetBalance);

        var result = await Get<BalanceView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("address", address)
        }));
        return result;
    }

    public async Task<TransactionIsCompletedView> TransactionIsCompleted(string hash, CancellationToken cancellationToken = default) {
        var path = GetPath(WalletServiceEnpointTypes.TransactionIsCompleted);

        var result = await Get<TransactionIsCompletedView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("hash", hash)
        }), cancellationToken);
        return result as TransactionIsCompletedView;
    }

    public async Task<object> TransactionMaxRate(string fromAddress, string privateKey, TransactionReceiverModel[] receivers) {
        var path = GetPath(WalletServiceEnpointTypes.CalculateMaxRate);
        var cmd = PrepareTransactionRequestBody(privateKey, receivers);

        var response = await Post<TransactionMaxRateModel>(path, cmd);
        return response as object;
    }

    public async Task<TransactionFeeView> TransactionFee(string from, string privateKey, TransactionReceiverModel[] receivers) {
        var path = GetPath(WalletServiceEnpointTypes.TransactionFee);

        var cmd = PrepareTransactionRequestBody(privateKey, receivers);

        var result = await Post<TransactionFeeView>(path, cmd);
        return result as TransactionFeeView;
    }

    public async Task<GenerateTransactionView> GenerateTransaction(string                     fromAddress, string privateKey,
                                                                   TransactionReceiverModel[] toAddresses) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var cmd = PrepareTransactionRequestBody(privateKey, toAddresses);

        var result = await Post<GenerateTransactionView>(path, cmd);
        return result as GenerateTransactionView;
    }

    #endregion

    #region Private Methods

    private object PrepareTransactionRequestBody(string signerPrivateKey, TransactionReceiverModel[] toAddresses) {
        var receivers = toAddresses.Select(a => (a.Address, a.Sum)).ToArray();
        return PrepareTransactionRequestBody(signerPrivateKey, receivers);
    }

    private object PrepareTransactionRequestBody(string signerPrivateKey, (string address, decimal sum)[] receivers) {
        var receiversArray = new object[receivers.Length];

        for (var i = 0; i < receiversArray.Length; i++) {
            receiversArray[i] = new {
                address = receivers[i].address,
                sum = receivers[i].sum
            };
        }

        var request = new {
            signerPrivateKey = signerPrivateKey,
            receivers = receiversArray
        };
        return request;
    }

    private async Task<T> Post<T>(string endpointPath, object requestValue, CancellationToken cancellationToken = default) where T : class {
        using var client = new HttpClient(new HttpClientHandler());
        AddHeaders(client);
        var json = JsonSerializer.Serialize(requestValue);
        var result = await SendPostJson(client, endpointPath, requestValue, cancellationToken);

        if (result.IsSuccessStatusCode)
            return DeserializeContent<T>(result.Content);

        throw new HttpRequestException(result.Content);
    }

    private T DeserializeContent<T>(string responceContent) where T : class {
        if (!string.IsNullOrWhiteSpace(responceContent))
            return null as T;

        return JsonConvert.DeserializeObject<T>(responceContent);
    }

    private async Task<T> Put<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                 CancellationToken cancellationToken = default) where T : class {
        using var client = new HttpClient(new HttpClientHandler());
        AddHeaders(client);

        var response = await SendPutRequest<T>(client, endpointPath, queryProperties, null,
            cancellationToken);
        return response;
    }

    private async void AddHeaders(HttpClient client) {
        client.DefaultRequestHeaders.Add(_walletServiceOptions.HeaderPrivateKeyOptionName, _walletServiceOptions.PrivateKey);
        client.DefaultRequestHeaders.Add("origin", _walletServiceOptions.Origin);
    }

    private async Task<T> Get<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                 CancellationToken cancellationToken = default) where T : class {
        using var client = new HttpClient(new HttpClientHandler());
        AddHeaders(client);
        var response = await SendGetRequest<T>(client, endpointPath, queryProperties, cancellationToken);
        return response;
    }

    private async Task<T> SendPutRequest<T>(HttpClient                  client,
                                            string                      endPoint,
                                            Dictionary<string, string>? queryProperties   = null,
                                            HttpContent?                httpContent       = null,
                                            CancellationToken           cancellationToken = default) where T : class {
        using (client) {
            var uri = AddQueryProperties(endPoint, queryProperties);
            var response = await client.PutAsync(uri, httpContent, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
                return DeserializeContent<T>(content);

            throw new HttpRequestException(content);
        }
    }

    private Uri AddQueryProperties(string url, Dictionary<string, string>? queryProperties = null) {
        if (queryProperties?.Count > 0) {
            url += "?";
            var pattern = "[?&]$";
            var rgx = new Regex(pattern);

            foreach (var key in queryProperties.Keys) {
                var urlComponent = HttpUtility.UrlEncode(queryProperties[key]);
                url += $"{key}={urlComponent}&";
                url = rgx.Replace(url, "");
            }
        }

        return new Uri(url);
    }

    private async Task<T> SendGetRequest<T>(HttpClient                  client,
                                            string                      endPoint,
                                            Dictionary<string, string>? queryProperties   = null,
                                            CancellationToken           cancellationToken = default) where T : class {
        using (client) {
            var uri = AddQueryProperties(endPoint, queryProperties);

            var response = await client.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
                return DeserializeContent<T>(content);

            throw new HttpRequestException(content);
        }
    }

    private async Task<(string Content, HttpStatusCode Code, bool IsSuccessStatusCode)> SendPostJson(HttpClient client,
        string endPoint,
        object entity,
        CancellationToken cancellationToken = default) {
        var url = new Uri(endPoint);
        var response = await client.PostAsJsonAsync(url, entity, cancellationToken);
        var resultCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return (content, resultCode, response.IsSuccessStatusCode);
    }

    private bool IsSuccessStatusCode(int status) {
        return status is >= 200 and <= 299;
    }

    private bool IsSuccessStatusCode(HttpStatusCode status) {
        return IsSuccessStatusCode((int)status);
    }

    #endregion
}