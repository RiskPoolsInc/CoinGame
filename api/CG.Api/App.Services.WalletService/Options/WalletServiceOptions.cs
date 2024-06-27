using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;
using App.Services.WalletService;

namespace App.Services.Telegram.Options;

[ConfigurationName("WalletServiceOptions")]
public class WalletServiceOptions : IConfig {
    public static string SECTION_NAME = "WalletServiceOptions";
    public string Host { get; set; }
    public string PrivateKey { get; set; }
    public WalletServiceEnpoint[] Enpoints { get; set; }
}