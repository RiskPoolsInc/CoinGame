using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;

using App.Interfaces.Security;
using App.Services.UbikiriApiService.Configurations;

using Newtonsoft.Json;

namespace App.Services.UbikiriApiService;

public class UbikiriApiService {
    private readonly IContextProvider _contextProvider;
    private readonly UbikiriSettings _ubikiriSettings;

    public UbikiriApiService(IContextProvider contextProvider, UbikiriSettings ubikiriSettings) {
        _contextProvider = contextProvider;
        _ubikiriSettings = ubikiriSettings;
        AccessToken = contextProvider.GetAccessToken();
    }

    public string? AccessToken { get; set; }
    private object? UserData { get; set; }

    #region Public Methods
    

    #endregion

    #region BaseMethods

    private async Task<object> Post<T>(string endpointPath, object requestValue, CancellationToken cancellationToken) {
        using var client = new HttpClient(new HttpClientHandler());
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
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
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

        var response = await SendGetRequest<T>(client, endpointPath, queryProperties, cancellationToken);
        return response;
    }

    private async Task<object> SendGetRequest<T>(HttpClient                  client,
                                                 string                      endPoint,
                                                 Dictionary<string, string>? queryProperties   = null,
                                                 CancellationToken           cancellationToken = default) {
        using (client) {
            var url = endPoint;

            if (queryProperties?.Count > 0) {
                url += "?";
                var pattern = "[?&]$";
                var rgx = new Regex(pattern);

                foreach (var key in queryProperties.Keys) {
                    var urlComponent = HttpUtility.UrlEncode(queryProperties[key]);
                    url += $"{key}={urlComponent}&";
                }

                url = rgx.Replace(url, pattern);
            }

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