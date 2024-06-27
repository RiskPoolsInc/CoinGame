using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.Profiles;

public class UserProfileByUbikiriUserId : ACriteria<UserProfile>
{
    private readonly Guid _ubikiriUserId;

    public UserProfileByUbikiriUserId(Guid ubikiriUserId)
    {
        _ubikiriUserId = ubikiriUserId;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        return a => a.UbikiriUserId == _ubikiriUserId;
    }
}