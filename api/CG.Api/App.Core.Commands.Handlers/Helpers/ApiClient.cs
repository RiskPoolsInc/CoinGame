using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.Core.Commands.Handlers.Helpers {

public class ApiClient
{
    private readonly string accessToken;
    private readonly string baseAddress;
    private readonly JsonSerializerSettings jsonSettings;

    public ApiClient(string baseAddress = null, string accessToken = null)
    {
        this.baseAddress = baseAddress;
        this.accessToken = accessToken;
        jsonSettings = new JsonSerializerSettings();

        jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        jsonSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    }

    public async Task<(HttpStatusCode, TOut)> GetAsync<TOut>(object message, string path, CancellationToken cancellationToken)
    {
        return await DoAsync<TOut>(message, path, (client, path, content) => client.GetAsync(path, cancellationToken));
    }

    public async Task<(HttpStatusCode, TOut)> PostAsync<TOut>(object message, string path, CancellationToken cancellationToken)
    {
        return await DoAsync<TOut>(message, path, (client, path, content) => client.PostAsync(path, content, cancellationToken));
    }

    public async Task<(HttpStatusCode, TOut)> PutAsync<TOut>(object message, string path, CancellationToken cancellationToken)
    {
        return await DoAsync<TOut>(message, path, (client, path, content) => client.PutAsync(path, content, cancellationToken));
    }

    protected async Task<(HttpStatusCode, TOut)> DoAsync<TOut>(object message,
        string path,
        Func<HttpClient, string, StringContent, Task<HttpResponseMessage>> method
    )
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            if (!string.IsNullOrWhiteSpace(accessToken)) client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var payload = JsonConvert.SerializeObject(message);

            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await method(client, path, httpContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TOut>(content, jsonSettings);
            return (response.StatusCode, result);
        }
    }
}
}