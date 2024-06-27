using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileByTypeId : ACriteria<UserProfile>
{
    private readonly int _typeId;

    public UserProfileByTypeId(int typeId)
    {
        _typeId = typeId;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        return a => a.TypeId == _typeId;
    }
}