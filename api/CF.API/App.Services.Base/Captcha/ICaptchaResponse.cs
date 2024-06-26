using System.Text.Json.Serialization;

namespace App.Services.Base.Captcha;

//{
//     "success": true|false,     // is the passcode valid, and does it meet security criteria you specified, e.g. sitekey?
//     "challenge_ts": timestamp, // timestamp of the challenge (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
//     "hostname": string,        // the hostname of the site where the challenge was solved
//     "credit": true|false,      // optional: whether the response will be credited
//     "error-codes": [...]       // optional: any error codes
//     "score": float,            // ENTERPRISE feature: a score denoting malicious activity.
//     "score_reason": [...]      // ENTERPRISE feature: reason(s) for score.
// }
public interface ICaptchaResponse {
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