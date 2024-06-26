using App.Data.Entities.UserLogs;
using App.Interfaces.Repositories;

namespace App.Repositories.UserLogs;

public class UserLogRepository : Repository<UserLog>, IUserLogRepository {
    public UserLogRepository(IAppDbContext context) : base(context) {
    }
}