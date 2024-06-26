using App.Data.Entities.Pbz.Notifications;
using App.Interfaces.Repositories.Pbz.Notifications;

namespace App.Repozitories.Pbz.Notifications {

public class UserFollowRepository : Repository<UserFollow>, IUserFollowRepository
{
    public UserFollowRepository(IPbzDbContext context) : base(context)
    {
    }
}
}