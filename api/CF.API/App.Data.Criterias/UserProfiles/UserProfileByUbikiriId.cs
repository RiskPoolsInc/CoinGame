using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileByUbikiriId : ACriteria<UserProfile>
{
    private readonly Guid _ubikiriUserId;

    public UserProfileByUbikiriId(Guid ubikiriUserId)
    {
        _ubikiriUserId = ubikiriUserId;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        return a => a.UbikiriUserId == _ubikiriUserId;
    }
}