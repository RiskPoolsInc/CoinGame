using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;

using App.Common.Helpers;
using App.Core.Configurations;
using App.Core.Exceptions;

using Newtonsoft.Json;

namespace App.Services.WalletService;

public class UbikiriWalletService {
    private readonly AccountUbikiriConfig _accountUbikiriConfig;

    public UbikiriWalletService(AccountUbikiriConfig accountUbikiriConfig) {
        _accountUbikiriConfig = accountUbikiriConfig;
    }

    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    private object? UserData { get; set; }
    private int? ExpiredIn { get; set; }
    private DateTime? ExpiredDate { get; set; }

    private async Task CheckAuthorization() {
        if (string.IsNullOrWhiteSpace(AccessToken))
            await Login();

        if (DateTime.UtcNow > ExpiredDate)
            await RenewRefreshToken();
    }

    #region Public Methods

    public async Task<string> CreateWallet() {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateBalance(string hashAddress) {
        throw new NotImplementedException();
    }

    public async Task<object> TransactionToUserWallet(object from, object to) {
        throw new NotImplementedException();
    }

    #endregion

    #region BaseMethods

    private async Task<object> Post<T>(string endpointPath, object requestValue, CancellationToken cancellationToken) {
        await CheckAuthorization();

        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        var result = await SendPostJson(client, endpointPath, requestValue, cancellationToken);

        if (result.IsSuccessStatusCode)
            return !string.IsNullOrWhiteSpace(result.Content)
                ? JsonConvert.DeserializeObject(result.Content, typeof(T))
                : result.Content;

        throw new HttpRequestException(result.Content);
    }

    private async Task<object> Get<T>(string            endpointPath, Dictionary<string, string>? queryProperties = null,
                                      CancellationToken cancellationToken = default) {
        await CheckAuthorization();

        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

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

    #region Authorization

    public async Task Login(CancellationToken cancellationToken = default) {
        await SendAuthRequest(_accountUbikiriConfig.LoginPath,
                              new {
                                  email = _accountUbikiriConfig.Login,
                                  password = _accountUbikiriConfig.Password
                              }, cancellationToken);
    }

    public async Task RenewRefreshToken(CancellationToken cancellationToken = default) {
        await SendAuthRequest(_accountUbikiriConfig.RenewTokenPath, new {
            token = RefreshToken
        }, cancellationToken);
    }

    private async Task<LoginResponse> SendAuthRequest(string path, object body, CancellationToken cancellationToken) {
        var apiClient = new ApiClient(path);

        try {
            var (statusCode, response) = await apiClient.PostAsync<LoginResponse>(body, "", cancellationToken);

            if (IsSuccessStatusCode(statusCode)) {
                AccessToken = response.access_token;
                RefreshToken = response.refresh_token;
                ExpiredDate = response.issued;
                ExpiredIn = response.expires_in;
                return response;
            }

            throw new BadRequestException($"Error Auth Request: receive not success status code {(int)statusCode}");
        }
        catch (BadRequestException) {
            throw;
        }
        catch (Exception e) {
            throw new BadRequestException($"Error Auth Request: {e.Message}");
        }
    }

    private class LoginResponse {
        public string access_token { get; }
        public string refresh_token { get; }
        public int? expires_in { get; }
        public DateTime? issued { get; }
    }

    #endregion
}