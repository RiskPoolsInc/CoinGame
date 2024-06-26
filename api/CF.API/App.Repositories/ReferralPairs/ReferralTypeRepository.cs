using App.Interfaces.Repositories;

namespace App.Repositories.ReferralPairs;

public class ReferralTypeRepository : DictionaryRepository<ReferralType>, IReferralTypeRepository {
    public ReferralTypeRepository(IAppDbContext context) : base(context) {
    }
}