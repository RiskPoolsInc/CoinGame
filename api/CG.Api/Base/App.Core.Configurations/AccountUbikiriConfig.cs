using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

namespace App.Core.Configurations;

[ConfigurationName("AccountUbikiri")]
public class AccountUbikiriConfig : IConfig {
    public string Host { get; set; }
    public string SocialHost { get; set; }
    public string LoginPath { get; set; }
    public string RenewTokenPath { get; set; }
    public string CreateWalletPath { get; set; }
    public string SendTransactionPath { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
}