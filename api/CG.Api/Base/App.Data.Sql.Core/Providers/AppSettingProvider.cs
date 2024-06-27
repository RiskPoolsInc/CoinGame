using App.Common;
using App.Interfaces.Core;
using Microsoft.Extensions.Configuration;

namespace App.Data.Sql.Core.Providers;

public class AppSettingProvider : IAppSettings
{
    public static string SETTING_NAME => new AppSettingProvider().SettingName;
    public string SettingName => "AppSettings";
    public IRejectingSettings BlockUserRejects { get; set; }
    public IWebAppSettings Web { get; set; }

    public static AppSettingProvider Get(IConfiguration configuration)
    {
        var rejectingSettings = configuration.GetSection(SETTING_NAME)
            .GetSection(nameof(IAppSettings.BlockUserRejects)).Get<RejectingSettings>();

        var webSettings = configuration.GetSection(SETTING_NAME)
            .GetSection(nameof(IAppSettings.Web)).Get<WebAppSettings>();

        return new AppSettingProvider()
        {
            BlockUserRejects = rejectingSettings,
            Web = webSettings
        };
    }
}