using System.Text.Json.Serialization;

namespace App.Services.Base.Captcha;

public class CaptchaResponse : ICaptchaResponse {
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")]
    public DateTime ChallengeTs { get; set; }

    [JsonPropertyName("hostname")]
    public string Hostname { get; set; }

    [JsonPropertyName("credit")]
    public bool Credit { get; set; }

    [JsonPropertyName("error-codes")]
    public string[] ErrorCodes { get; set; }

    [JsonPropertyName("score")]
    public decimal Score { get; set; }

    [JsonPropertyName("score_reason")]
    public string[] ScoreReason { get; set; }
}