using System.Text.Json.Serialization;

namespace App.Services.Base.Captcha;

public interface ICaptchaRequest {
    [JsonPropertyName("response")]
    public string response { get; set; }

    [JsonPropertyName("secret")]
    public string secret { get; set; }
}