namespace App.Interfaces.ExternalServices;

public interface IWalletService {
    /// <summary>
    ///     Create wallet
    /// </summary>
    /// <param name="name">Wallet name</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Wallet hash address</returns>
    public Task<string> CreateWallet(string name, CancellationToken cancellationToken);

    /// <summary>
    ///     Get wallet balance
    /// </summary>
    /// <param name="address">Wallet hash address</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Wallet balance</returns>
    public Task<decimal> GetBalance(string address, CancellationToken cancellationToken);
}