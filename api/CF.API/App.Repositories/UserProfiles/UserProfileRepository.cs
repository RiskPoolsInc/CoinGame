using App.Data.Entities.UserProfiles;
using App.Interfaces.Repositories;

namespace App.Repositories.UserProfiles;

public class UserProfileRepository : ArchivableRepository<UserProfile>, IUserProfileRepository {
    public UserProfileRepository(IAppDbContext appDbContext) : base(appDbContext) {
    }
}