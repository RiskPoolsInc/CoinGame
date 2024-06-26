using App.Data.Entities.Notifications;

namespace App.Repositories.Notifications; 

public class ReferralPairFollowRepository : Repository<ReferralPairFollow>, IReferralPairFollowRepository {
    public ReferralPairFollowRepository(IAppDbContext context) : base(context) {
    }
}