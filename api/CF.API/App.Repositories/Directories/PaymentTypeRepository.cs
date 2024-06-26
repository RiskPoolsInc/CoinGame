namespace App.Repositories.Directories;

public class PaymentTypeRepository : DictionaryRepository<TransactionType>, IPaymentTypeRepository {
    public PaymentTypeRepository(IAppDbContext context) : base(context) {
    }
}