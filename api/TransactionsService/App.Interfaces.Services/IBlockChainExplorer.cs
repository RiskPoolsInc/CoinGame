namespace App.Interfaces.Services;

public interface IBlockChainExplorer
{
    Task<bool> CheckExistHash(string hash, CancellationToken cancellationToken
        );
    Task<object> GetHashData(string hash, CancellationToken cancellationToken);
}