using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;

using App.Core.ViewModels.External;
using App.Services.Telegram.Options;
using App.Services.WalletService.Helpers;
using App.Services.WalletService.Models;

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

    private ServiceWallet GetWallet(ServiceWalletTypes type) => _systemSettingsOptions.Wallets.Single(a => a.Type == (int)type);

    private string GetWalletAddress(ServiceWalletTypes type) => GetWallet(type).Value;

    #endregion

    #region EndpointPath

    private string BasePath => $"{_walletServiceOptions.Host}";

    private WalletServiceEnpoint ServiceEndpoint(WalletServiceEnpointTypes typeId) =>
        _walletServiceOptions.Endpoints.Single(a => a.Type == (int)typeId);

    private string GetPath(WalletServiceEnpointTypes type) => $"{BasePath}/{ServiceEndpoint(type).Value}";

    #endregion

    #region Public Methods

    public async Task<GeneratedWalletView> GenerateWallet() {
        var path = GetPath(WalletServiceEnpointTypes.GenerateWallet);
        var result = await Put<GeneratedWalletView>(path);
        return result;
    }

    public async Task<BalanceView> GetBalance(Guid importedWalletId) {
        var path = GetPath(WalletServiceEnpointTypes.GetBalance);

        var result = await Get<BalanceView>(path, new Dictionary<string, string>(new[] {
            new KeyValuePair<string, string>("walletId", importedWalletId.ToString("D"))
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

    public async Task<object> TransactionMaxRate(Guid importedWalletId, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.CalculateMaxRate);
        var commission = sum * 0.02m;
        var gameDeposit = sum - commission;

        var cmd = PrepareTransactionRequestBody(importedWalletId, new ReceiverCoinsModel[] {
            new(GetWalletAddress(ServiceWalletTypes.GameDeposit), -1),
            new(GetWalletAddress(ServiceWalletTypes.Commission), commission),
        });

        var result = await Post<object>(path, cmd);
        return result as object;
    }

    public async Task<TransactionFeeView> TransactionFee(Guid importedWalletId, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.TransactionFee);
        var commission = sum * 0.02m;
        var gameDeposit = sum - commission;

        var cmd = PrepareTransactionRequestBody(importedWalletId, new ReceiverCoinsModel[] {
            new(GetWalletAddress(ServiceWalletTypes.GameDeposit), gameDeposit),
            new(GetWalletAddress(ServiceWalletTypes.Commission), commission),
        });

        var result = await Post<TransactionFeeView>(path, cmd);
        return result as TransactionFeeView;
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
            PrepareTransactionRequestBody(Guid.Parse(gameDepositWallet.Value), new[] {
                new ReceiverCoinsModel(serviceWallet.Value, servicePaymentSum)
            }));

        var result = response as GenerateTransactionView;
        result.WalletFrom = gameDepositWallet.Value;
        return result;
    }

    private object PrepareTransactionRequestBody(Guid walletId, IEnumerable<ReceiverCoinsModel> receivers) {
        var receiversObjectList = new List<object>();

        foreach (var receiverModel in receivers) {
            receiversObjectList.Add(new {
                address = receiverModel.Address,
                sum = receiverModel.Sum
            });
        }

        var request = new {
            walletId = walletId,
            receivers = receiversObjectList.ToArray()
        };
        return request;
    }

    public async Task<GenerateTransactionView> GenerateTransactionGameDeposit(Guid importeedWalletId, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var commission = sum * 0.02m;
        var gameDeposit = sum - commission;

        var cmd = PrepareTransactionRequestBody(importeedWalletId, new ReceiverCoinsModel[] {
            new(GetWalletAddress(ServiceWalletTypes.GameDeposit), gameDeposit),
            new(GetWalletAddress(ServiceWalletTypes.Commission), commission),
        });

        var result = await Post<GenerateTransactionView>(path, cmd);
        return result as GenerateTransactionView;
    }

    public async Task<GenerateTransactionView> GenerateTransactionRefund(Guid importedWalletId) {
        var path = GetPath(WalletServiceEnpointTypes.RefundCoins);

        var result = await Post<GenerateTransactionView>(path, new {
            id = importedWalletId
        });
        return result;
    }

    public async Task<TransactionGameRewardView[]> GenerateTransactionRewards(GameRewardReceiverModel[] receivers) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var walletFromAddress = GetWalletAddress(ServiceWalletTypes.Reward);
        var commissionAddress = GetWalletAddress(ServiceWalletTypes.Commission);

        var gamesRewards = new List<TransactionGameRewardView>();
        var rewardsReceivers = new List<ReceiverCoinsModel>();
        var groupedByAddress = receivers.GroupBy(a => a.Address);
        var commission = 0m;
        var commissionPercent = 0.02m;

        foreach (var gameRewardReceiverModel in groupedByAddress) {
            var receiverAddress = gameRewardReceiverModel.Key;
            var sumReward = gameRewardReceiverModel.Sum(a => a.Sum);

            var userGamesRewards = gameRewardReceiverModel.Select(a => new TransactionGameRewardView {
                Hash = null,
                Sum = a.Sum * (100m - commissionPercent),
                WalletFrom = walletFromAddress,
                ReceiverAddress = receiverAddress,
                GameId = a.GameId,
            });

            var rewardsReceiver = new ReceiverCoinsModel {
                Address = receiverAddress,
                Sum = userGamesRewards.Sum(a => a.Sum),
            };
            commission += sumReward - rewardsReceiver.Sum;
        }

        var commissionReceiver = new ReceiverCoinsModel {
            Address = commissionAddress,
            Sum = commission,
        };
        rewardsReceivers.Add(commissionReceiver);
        var generateTransactionRequest = PrepareTransactionRequestBody(Guid.Parse(walletFromAddress), rewardsReceivers.ToArray());
        var generateTransactionView = await Post<GenerateTransactionView>(path, generateTransactionRequest);

        foreach (var transactionGameRewardView in gamesRewards)
            transactionGameRewardView.Hash = generateTransactionView.Hash;

        return gamesRewards.ToArray();
    }

    public async Task<GenerateTransactionView> GenerateTransactionReward(string toWallet, decimal sum) {
        var path = GetPath(WalletServiceEnpointTypes.GenerateTransaction);
        var fromAddress = GetWalletAddress(ServiceWalletTypes.Reward);
        var commissionService = GetWalletAddress(ServiceWalletTypes.Commission);

        var cmd = PrepareTransactionRequestBody(Guid.Parse(fromAddress), new ReceiverCoinsModel[] {
            new(toWallet, sum * 0.98m),
            new(commissionService, sum * 0.02m)
        });

        var response = await Post<GenerateTransactionView>(path, cmd);
        var result = response as GenerateTransactionView;
        result.WalletFrom = fromAddress;
        return result;
    }

    public bool NeedServiceTransaction() {
        return GetWalletAddress(ServiceWalletTypes.Service) != GetWalletAddress(ServiceWalletTypes.GameDeposit);
    }

    public string ProfitWalletId => GetWalletAddress(ServiceWalletTypes.Commission);

    #endregion

    #region BaseMethods

    private async void AddHeaders(HttpClient client) {
        client.DefaultRequestHeaders.Add(_walletServiceOptions.HeaderPrivateKeyOptionName, _walletServiceOptions.PrivateKey);
        client.DefaultRequestHeaders.Add("origin", _walletServiceOptions.Origin);
    }

    private async Task<T> Put<T>(string                      endPoint,
                                 Dictionary<string, string>? queryProperties   = null,
                                 HttpContent?                httpContent       = null,
                                 CancellationToken           cancellationToken = default) where T : class {
        using var client = NewHttpClient();
        var uri = AddQueryProperties(endPoint, queryProperties);
        var response = await client.PutAsync(uri, httpContent, cancellationToken);
        return await response.DecerializeResponce<T>();
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

    private async Task<object> Get<T>(string                      endPoint,
                                      Dictionary<string, string>? queryProperties   = null,
                                      CancellationToken           cancellationToken = default) where T : class {
        using var client = NewHttpClient();
        var uri = AddQueryProperties(endPoint, queryProperties);
        var response = await client.GetAsync(uri, cancellationToken);
        return response.DecerializeResponce<T>();
    }

    private async Task<T> Post<T>(string            endPoint, object entity,
                                  CancellationToken cancellationToken = default) where T : class {
        using var client = NewHttpClient();
        var url = new Uri(endPoint);
        var response = await client.PostAsJsonAsync(url, entity, cancellationToken);
        return await response.DecerializeResponce<T>();
    }

    private HttpClient NewHttpClient() {
        var client = new HttpClient(new HttpClientHandler());
        AddHeaders(client);
        return client;
    }

    #endregion
}