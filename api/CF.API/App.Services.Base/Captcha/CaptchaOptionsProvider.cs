using App.Interfaces.Services;

using Microsoft.Extensions.Configuration;

namespace App.Services.Base.Captcha;

public class CaptchaOptionsProvider : ICaptchaOptions {
    public const string SETTING_NAME = "CaptchaOptions";
    public string? SecretKey { get; set; }
    public string VerificationHost { get; set; }

    public static CaptchaOptionsProvider Get(IConfiguration configuration) {
        var verificationHost = configuration[$"{SETTING_NAME}:{nameof(VerificationHost)}"];
        var recaptchaSiteKey = configuration[$"{SETTING_NAME}:{nameof(SecretKey)}"];

        return new CaptchaOptionsProvider { VerificationHost = verificationHost, SecretKey = recaptchaSiteKey };
    }
}