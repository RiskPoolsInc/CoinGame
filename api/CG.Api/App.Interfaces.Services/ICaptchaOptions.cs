namespace App.Interfaces.Services;

public interface ICaptchaOptions {
    string SecretKey { get; set; }

    /// <summary>
    ///     Api endpoint for send SiteKey and generated token to check Token
    /// </summary>
    string VerificationHost { get; set; }
}