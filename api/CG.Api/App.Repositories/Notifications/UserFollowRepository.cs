using App.Data.Entities.Notifications;

namespace App.Repositories.Notifications; 

public class UserFollowRepository : Repository<UserFollow>, IUserFollowRepository {
    public UserFollowRepository(IAppDbContext context) : base(context) {
    }
}