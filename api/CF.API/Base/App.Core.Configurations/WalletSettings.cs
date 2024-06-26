using App.Interfaces.Core.Configurations;

namespace App.Core.Configurations; 

/// <summary>
///     Identity options contract
/// </summary>
public class WalletSettings : IConfig {
    /// <summary>
    ///     Wallet address for deposits
    /// </summary>
    public string Address { get; set; }
}