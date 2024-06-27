using System.Text.Json;

using App.Interfaces.Services;

namespace App.Services.Base.Captcha;

public class CaptchaService {
    private readonly ICaptchaOptions _captchaOptions;

    public CaptchaService(ICaptchaOptions captchaOptions) {
        _captchaOptions = captchaOptions;
    }

    public async Task<ICaptchaResponse> CreateAssessment(string token) {
        var siteKey = _captchaOptions.SecretKey;
        var apiEndpoint = _captchaOptions.VerificationHost;


        var requestData = new CaptchaRequest {
            response = token,
            secret = siteKey
        };

        using var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, apiEndpoint);

        var collection = new List<KeyValuePair<string, string>> {
            new("response", requestData.response),
            new("secret", requestData.secret)
        };
        var content = new FormUrlEncodedContent(collection);
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseValue = JsonSerializer.Deserialize<CaptchaResponse>(responseContent);

        if (responseValue.Success)
            return responseValue;
        throw new Exception("Invalid captcha request");
    }
}