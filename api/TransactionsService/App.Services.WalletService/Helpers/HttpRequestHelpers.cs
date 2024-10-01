using Newtonsoft.Json;

namespace App.Services.WalletService.Helpers;

public static class HttpRequestHelpers {
    public static async Task<T> DecerializeResponce<T>(this HttpResponseMessage response) where T : class {
        var content = await response.Content.ReadAsStringAsync(default);

        if (response.IsSuccessStatusCode) {
            if (string.IsNullOrWhiteSpace(content))
                return null as T;
            return JsonConvert.DeserializeObject<T>(content);
        }

        throw new HttpRequestException(content);
    }
}