using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;

using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Configurations;
using App.Core.Exceptions;
using App.Core.ViewModels.External;
using App.Services.Telegram.Options;

using Newtonsoft.Json;

namespace App.Services.WalletService;

public class WalletService {
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

    private string BasePath => $"{_walletServiceOptions.Host}/{_privateKey}";

    private WalletServiceEnpoint ServiceEndpoint(WalletServiceEnpointTypes typeId) =>
        _walletServiceOptions.Enpoints.Single(a => a.Type == (int)typeId);

    private string GetPath(WalletServiceEnpointTypes type) => $"{BasePath}/{ServiceEndpoint(type)}";

    #endregion

    #region Public Methods

    public async Task<GeneratedWalletView> GenerateWallet() {
        var path = GetPath(WalletServiceEnpointTypes.GenerateWallet);
        var result = await Get<GeneratedWalletView>(path);
        return result as GeneratedWalletView;
    }

    public async Task<BalanceView> GetBalance(string address) {
        var path = GetPath(WalletServiceEnpointTypes.GetBalance);

        var result = await Get<BalanceView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("Wallet", address)
        }));
        return result as BalanceView;
    }

    public async Task<TransactionIsCompletedView> CheckTransactionIsCompleted(string hash) {
        var path = GetPath(WalletServiceEnpointTypes.TransactionIsCompleted);

        var result = await Get<TransactionIsCompletedView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("TransactionHash", hash)
        }));
        return result as TransactionIsCompletedView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionService() {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var gameDepositWallet = GetWallet(ServiceWalletTypes.GameDeposit);
        var serviceWallet = GetWallet(ServiceWalletTypes.GameDeposit);

        var result = await Post<GenerateTransactionView>(path, new {
            WalletFrom = gameDepositWallet.Value,
            PrivateKey = gameDepositWallet.PrivateKey,
            WalletTo = serviceWallet.Value
        });
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionGameDeposit(string from, string privateKey, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);

        var cmd = new SendGenerateTransactionCommand {
            FromWallet = from,
            PrivateKey = privateKey,
            ToWallets = new TransactionReceiverView[] {
                new() {
                    Hash = GetWalletAddress(ServiceWalletTypes.GameDeposit),
                    Sum = sum
                }
            }
        };

        var result = await Post<GenerateTransactionView>(path, new SendGenerateTransactionCommand() {
        });
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionRefund(string from, string privateKey) {
        var path = GetPath(WalletServiceEnpointTypes.RefundCoins);

        var result = await Post<GenerateTransactionView>(path, new {
            FromWallet = from,
            PrivateKey = privateKey
        });
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionReward(string toWallet) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var walletFromAddress = GetWalletAddress(ServiceWalletTypes.Reward);
        var walletFromPrivateKey = GetWalletPrivateKey(ServiceWalletTypes.Reward);

        var cmd = new SendTransactionRewardCommand() {
            FromWallet = walletFromAddress,
            PrivateKey = walletFromPrivateKey,
            ToWallet = toWallet
        };

        var result = await Post<GenerateTransactionView>(path, cmd);
        return result as GenerateTransactionView;
    }

    #endregion

    #region BaseMethods

    private async Task<object> Post<T>(string endpointPath, object requestValue, CancellationToken cancellationToken = default) {
        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Add("AuthorizationToken", _walletServiceOptions.PrivateKey);
        var result = await SendPostJson(client, endpointPath, requestValue, cancellationToken);

        if (result.IsSuccessStatusCode)
            return !string.IsNullOrWhiteSpace(result.Content)
                ? JsonConvert.DeserializeObject(result.Content, typeof(T))
                : result.Content;

        throw new HttpRequestException(result.Content);
    }

    private async Task<object> Get<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                      CancellationToken cancellationToken = default) {
        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Add("AuthorizationToken", _walletServiceOptions.PrivateKey);

        var response = await SendGetRequest<T>(client, endpointPath, queryProperties, cancellationToken);
        return response;
    }

    private async Task<object> SendGetRequest<T>(HttpClient                  client,
                                                 string                      endPoint,
                                                 Dictionary<string, string>? queryProperties   = null,
                                                 CancellationToken           cancellationToken = default) {
        using (client) {
            var url = endPoint + "?";
            var pattern = "[?&]$";
            var rgx = new Regex(pattern);

            if (queryProperties?.Count > 0)
                foreach (var key in queryProperties.Keys) {
                    var urlComponent = HttpUtility.UrlEncode(queryProperties[key]);
                    url += $"{key}={urlComponent}&";
                }

            url = rgx.Replace(url, pattern);
            var uri = new Uri(url);

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