using Newtonsoft.Json;

namespace App.Core.ViewModels.Security {

public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("issued")]
    public DateTime? IssueDate { get; set; }

    [JsonProperty("expires_in")]
    public int? ExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("mfa_required")]
    public bool IsMfaRequired { get; set; }
}
}