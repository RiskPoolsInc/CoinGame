using App.Data.Entities.Referal;
using App.Interfaces.Repositories;

namespace App.Repositories.ReferralPairs;

public class ReferralPairRepository : AuditableRepository<ReferralPair>, IReferralPairRepository {
    public ReferralPairRepository(IAppDbContext appDbContext) : base(appDbContext) {
    }
}