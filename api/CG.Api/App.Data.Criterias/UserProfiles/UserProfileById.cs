using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileById : ACriteria<UserProfile>
{
    private readonly Guid _id;

    public UserProfileById(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        return a => a.Id == _id;
    }
}