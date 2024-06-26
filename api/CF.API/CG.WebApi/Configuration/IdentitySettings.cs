namespace CF.WebApi.Configuration; 

/// <summary>
///     Identity options contract
/// </summary>
public class IdentitySettings {
    /// <summary>
    ///     Url
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Key
    /// </summary>
    public string SecretJwtKey { get; set; }
}