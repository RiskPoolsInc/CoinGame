using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;
using App.Services.WalletService;

namespace App.Services.Telegram.Options;

[ConfigurationName("SystemSettings")]
public class SystemSettingsOptions : IConfig {
    public static string SECTION_NAME = "SystemSettings";
    public ServiceWallet[] Wallets { get; set; }
}