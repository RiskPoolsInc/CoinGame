using System.Net;
using System.Net.Http.Json;

using Newtonsoft.Json;

namespace App.Services.ExternalSystemRequests;

public class ExternalSystemSender {
    private readonly string _host;
    private readonly string _secretKey;

    public ExternalSystemSender(string host, string secretKey) {
        _host = host;
        _secretKey = secretKey;
    }

    public async Task<object> SendPostRequest<T>(string            endPoint, T entity, Type responseType,
                                                 CancellationToken cancellationToken = default) {
        var handler = new HttpClientHandler();

        using (var client = new HttpClient(handler)) {
            var result = await SendPostRequest(client, endPoint, entity, cancellationToken);

            if (result.IsSuccessStatusCode)
                return !string.IsNullOrWhiteSpace(result.Content)
                    ? JsonConvert.DeserializeObject(result.Content, responseType)
                    : result.Content;

            throw new HttpRequestException(result.Content);
        }
    }

    private async Task<(string Content, HttpStatusCode Code, bool IsSuccessStatusCode)> SendPostRequest(HttpClient client,
        string endPoint,
        object entity,
        CancellationToken cancellationToken = default) {
        var url = new Uri(_host + "/" + _secretKey + '/' + endPoint);
        var response = await client.PostAsJsonAsync(url, entity, cancellationToken);
        var resultCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return (content, resultCode, response.IsSuccessStatusCode);
    }

    public async Task<TResult> SendPostRequest<T, TResult>(string endPoint, T entity, CancellationToken cancellationToken = default) {
        var handler = new HttpClientHandler();

        using (var client = new HttpClient(handler)) {
            var result = await SendPostRequest(client, endPoint, entity, cancellationToken);

            if (result.Code == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<TResult>(result.Content);

            throw new HttpRequestException(result.Content);
        }
    }

    public async Task<object> SendGetRequest(string endPoint, Type resultType, CancellationToken cancellationToken = default) {
        var handler = new HttpClientHandler();
        using var client = new HttpClient(handler);
        var url = new Uri(_host + "/" + endPoint);
        var response = await client.GetAsync(url, cancellationToken);
        var resultCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (resultCode == HttpStatusCode.OK)
            return JsonConvert.DeserializeObject(content, resultType);
        throw new HttpRequestException(content);
    }
}