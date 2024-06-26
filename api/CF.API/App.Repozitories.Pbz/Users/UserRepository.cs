using App.Data.Entities.Pbz.Users;
using App.Interfaces.Repositories.Pbz.Users;

namespace App.Repozitories.Pbz.Users {

public class UserRepository : AuditableRepository<User>, IUserRepository  
{
    public UserRepository(IPbzDbContext context) : base(context)
    {
    }
}
}