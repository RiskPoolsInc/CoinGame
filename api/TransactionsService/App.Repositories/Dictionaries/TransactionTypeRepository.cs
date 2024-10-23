namespace App.Repositories.Dictionaries;

public class TransactionTypeRepository : DictionaryRepository<TransactionType>, ITransactionTypeRepository {
    public TransactionTypeRepository(IAppDbContext context) : base(context) {
    }
}