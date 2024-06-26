using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileCreatedTo : ACriteria<UserProfile>
{
    private readonly DateTime _date;

    public UserProfileCreatedTo(DateTime date)
    {
        _date = date;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        return a => a.CreatedOn <= _date;
    }
}