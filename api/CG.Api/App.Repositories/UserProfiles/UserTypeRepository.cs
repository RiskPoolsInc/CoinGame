using App.Interfaces.Repositories;

namespace App.Repositories.UserProfiles;

public class UserTypeRepository : DictionaryRepository<UserType>, IUserTypeRepository {
    public UserTypeRepository(IAppDbContext context) : base(context) {
    }
}