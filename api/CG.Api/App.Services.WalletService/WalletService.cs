using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;

using App.Core.ViewModels.External;
using App.Services.Telegram.Options;

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

    #region ServiceWallet

    private ServiceWallet GetWallet(ServiceWalletTypes    type) => _systemSettingsOptions.Wallets.Single(a => a.Type == (int)type);
    private string GetWalletAddress(ServiceWalletTypes    type) => GetWallet(type).Value;
    private string GetWalletPrivateKey(ServiceWalletTypes type) => GetWallet(type).PrivateKey;

    #endregion

    #region EndpointPath

    private string BasePath => $"{_walletServiceOptions.Host}";

    private WalletServiceEnpoint ServiceEndpoint(WalletServiceEnpointTypes typeId) =>
        _walletServiceOptions.Endpoints.Single(a => a.Type == (int)typeId);

    private string GetPath(WalletServiceEnpointTypes type) => $"{BasePath}/{ServiceEndpoint(type).Value}";

    #endregion

    #region Public Methods

    public static string EncryptPrivateKey(string privateKey, string key) {
        return privateKey;
    }

    public static string DecrypPrivateKey(string privateKey, string key) {
        return privateKey;
    }

    public async Task<GeneratedWalletView> GenerateWallet() {
        var path = GetPath(WalletServiceEnpointTypes.GenerateWallet);
        var result = await Put<GeneratedWalletView>(path);
        return result as GeneratedWalletView;
    }

    public async Task<BalanceView> GetBalance(string address) {
        var path = GetPath(WalletServiceEnpointTypes.GetBalance);

        var result = await Get<BalanceView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("address", address)
        }));
        return result as BalanceView;
    }

    public async Task<TransactionIsCompletedView> CheckTransactionIsCompleted(string hash, CancellationToken cancellationToken = default) {
        var path = GetPath(WalletServiceEnpointTypes.TransactionIsCompleted);

        var result = await Get<TransactionIsCompletedView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("hash", hash)
        }), cancellationToken);
        return result as TransactionIsCompletedView;
    }

    public async Task<GenerateTransactionView> CalculateTransaction(string from, string privateKey, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.CalculateTransaction);
        var commission = sum * 0.02m;
        var gameDeposit = sum - commission;

        var cmd = PrepareTransactionRequestBody(privateKey, new (string address, decimal sum)[] {
            (GetWalletAddress(ServiceWalletTypes.GameDeposit), gameDeposit),
            (GetWalletAddress(ServiceWalletTypes.Commission), commission),
        });

        var result = await Post<GenerateTransactionView>(path, cmd);
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionService(decimal roundSum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var gameDepositWallet = GetWallet(ServiceWalletTypes.GameDeposit);
        var serviceWallet = GetWallet(ServiceWalletTypes.Service);
        var commissionWallet = GetWallet(ServiceWalletTypes.Commission);

        if (gameDepositWallet.Value == serviceWallet.Value)
            return null;

        var servicePaymentSum = roundSum * 0.784m;

        var response = await Post<GenerateTransactionView>(path,
            PrepareTransactionRequestBody(gameDepositWallet.PrivateKey,
                new (string address, decimal sum)[] {
                    (serviceWallet.Value, servicePaymentSum)
                }));

        var result = response as GenerateTransactionView;
        result.WalletFrom = gameDepositWallet.Value;
        return result;
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

    public async Task<GenerateTransactionView> GenerateTransactionGameDeposit(string from, string privateKey, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var commission = sum * 0.02m;
        var gameDeposit = sum - commission;

        var cmd = PrepareTransactionRequestBody(privateKey, new (string address, decimal sum)[] {
            (GetWalletAddress(ServiceWalletTypes.GameDeposit), gameDeposit),
            (GetWalletAddress(ServiceWalletTypes.Commission), commission),
        });

        var result = await Post<GenerateTransactionView>(path, cmd);
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionRefund(string from, string privateKey) {
        var path = GetPath(WalletServiceEnpointTypes.RefundCoins);

        var result = await Post<GenerateTransactionView>(path, new {
            signerPrivateKey = privateKey
        });
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionReward(string toWallet, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var walletFromAddress = GetWalletAddress(ServiceWalletTypes.Reward);
        var walletFromPrivateKey = GetWalletPrivateKey(ServiceWalletTypes.Reward);
        var commissionService = GetWalletAddress(ServiceWalletTypes.Commission);

        var cmd = PrepareTransactionRequestBody(walletFromPrivateKey, new (string address, decimal sum)[] {
            (toWallet, sum * 0.98m),
            (commissionService, sum * 0.02m)
        });

        var response = await Post<GenerateTransactionView>(path, cmd);
        var result = response as GenerateTransactionView;
        result.WalletFrom = walletFromAddress;
        return result;
    }

    public bool NeedServiceTransaction() {
        return GetWalletAddress(ServiceWalletTypes.Service) != GetWalletAddress(ServiceWalletTypes.GameDeposit);
    }

    public string ProfitWalletAddress => GetWalletAddress(ServiceWalletTypes.Commission);

    #endregion

    #region BaseMethods

    private async Task<object> Post<T>(string endpointPath, object requestValue, CancellationToken cancellationToken = default) {
        using var client = new HttpClient(new HttpClientHandler());
        AddHeaders(client);
        var json = JsonSerializer.Serialize(requestValue);
        var result = await SendPostJson(client, endpointPath, requestValue, cancellationToken);

        if (result.IsSuccessStatusCode)
            return !string.IsNullOrWhiteSpace(result.Content)
                ? JsonConvert.DeserializeObject(result.Content, typeof(T))
                : result.Content;

        throw new HttpRequestException(result.Content);
    }

    private async Task<object> Put<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                      CancellationToken cancellationToken = default) {
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

    private async Task<object> Get<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                      CancellationToken cancellationToken = default) {
        using var client = new HttpClient(new HttpClientHandler());
        AddHeaders(client);
        var response = await SendGetRequest<T>(client, endpointPath, queryProperties, cancellationToken);
        return response;
    }

    private async Task<object> SendPutRequest<T>(HttpClient                  client,
                                                 string                      endPoint,
                                                 Dictionary<string, string>? queryProperties   = null,
                                                 HttpContent?                httpContent       = null,
                                                 CancellationToken           cancellationToken = default) {
        using (client) {
            var uri = AddQueryProperties(endPoint, queryProperties);
            var response = await client.PutAsync(uri, httpContent, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
                return !string.IsNullOrWhiteSpace(content)
                    ? JsonConvert.DeserializeObject(content, typeof(T))
                    : response.Content;

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

    private async Task<object> SendGetRequest<T>(HttpClient                  client,
                                                 string                      endPoint,
                                                 Dictionary<string, string>? queryProperties   = null,
                                                 CancellationToken           cancellationToken = default) {
        using (client) {
            var uri = AddQueryProperties(endPoint, queryProperties);

            var response = await client.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
                return !string.IsNullOrWhiteSpace(content)
                    ? JsonConvert.DeserializeObject(content, typeof(T))
                    : response.Content;

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