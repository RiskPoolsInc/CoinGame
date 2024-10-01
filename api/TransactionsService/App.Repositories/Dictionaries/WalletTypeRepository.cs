namespace App.Repositories.Dictionaries;

public class WalletTypeRepository : DictionaryRepository<WalletType>, IWalletTypeRepository {
    public WalletTypeRepository(IAppDbContext context) : base(context) {
    }
}