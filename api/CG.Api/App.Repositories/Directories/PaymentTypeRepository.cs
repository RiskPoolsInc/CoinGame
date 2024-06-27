namespace App.Repositories.Directories;

public class PaymentTypeRepository : DictionaryRepository<TransactionType>, ITransactionTypeRepository {
    public PaymentTypeRepository(IAppDbContext context) : base(context) {
    }
}