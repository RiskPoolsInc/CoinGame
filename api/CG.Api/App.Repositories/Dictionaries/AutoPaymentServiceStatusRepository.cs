using App.Data.Sql.Core.Interfaces;

namespace App.Repositories.Dictionaries;

public class AutoPaymentServiceStatusRepository: DictionaryRepository<AutoPaymentServiceLogType>, IAutoPaymentServiceStatusRepository {
    public AutoPaymentServiceStatusRepository(IAppDbContext context) : base(context) {
    }
}