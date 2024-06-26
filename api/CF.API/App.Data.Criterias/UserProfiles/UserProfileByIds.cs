using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileByIds : ACriteria<UserProfile>
{
    private readonly Guid[] _ids;

    public UserProfileByIds(Guid[] ids)
    {
        _ids = ids;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        if (_ids.Length == 1)
            return new UserProfileById(_ids[0]);

        return a => _ids.Contains(a.Id);
    }
}