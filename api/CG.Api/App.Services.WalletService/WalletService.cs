using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using App.Core.Commands.Transactions;
using App.Core.ViewModels.External;
using App.Services.Telegram.Options;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

    public async Task<TransactionIsCompletedView> CheckTransactionIsCompleted(string hash) {
        var path = GetPath(WalletServiceEnpointTypes.TransactionIsCompleted);

        var result = await Get<TransactionIsCompletedView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("hash", hash)
        }));
        return result as TransactionIsCompletedView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionService() {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var gameDepositWallet = GetWallet(ServiceWalletTypes.GameDeposit);
        var serviceWallet = GetWallet(ServiceWalletTypes.GameDeposit);

        var result = await Post<GenerateTransactionView>(path, new {
            signerPrivateKey = gameDepositWallet.PrivateKey,
            toAddress = serviceWallet.Value
        });
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionGameDeposit(string from, string privateKey, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);

        // var cmd = new SendGenerateTransactionCommand {
        //     SignerPrivateKey = from,
        //     Receivers = new TransactionReceiverView[] {
        //         new() {
        //             Address = GetWalletAddress(ServiceWalletTypes.GameDeposit),
        //             Sum = sum
        //         }
        //     }
        // };

        var cmd = new {
            signerPrivateKey = privateKey,
            receivers = new object[] {
                GetWalletAddress(ServiceWalletTypes.GameDeposit), sum
            }
        };

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

        var cmd = new {
            signerPrivateKey = walletFromPrivateKey,
            receivers = new string[] {
                toWallet, sum.ToString()
            }
        };

        var result = await Post<GenerateTransactionView>(path, cmd);
        return result as GenerateTransactionView;
    }

    #endregion

    #region BaseMethods

    private async Task<object> Post<T>(string endpointPath, object requestValue, CancellationToken cancellationToken = default) {
        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Add("AuthorizationToken", _walletServiceOptions.PrivateKey);
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
        client.DefaultRequestHeaders.Add("AuthorizationToken", _walletServiceOptions.PrivateKey);

        var response = await SendPutRequest<T>(client, endpointPath, queryProperties, null,
            cancellationToken);
        return response;
    }

    private async Task<object> Get<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                      CancellationToken cancellationToken = default) {
        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Add("AuthorizationToken", _walletServiceOptions.PrivateKey);

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