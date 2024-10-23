namespace App.Repositories.Directories;

public class TransactionTypeRepository : DictionaryRepository<TransactionType>, ITransactionTypeRepository {
    public TransactionTypeRepository(IAppDbContext context) : base(context) {
    }
}