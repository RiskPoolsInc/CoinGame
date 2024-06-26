using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileCreatedFrom : ACriteria<UserProfile>
{
    private readonly DateTime _date;

    public UserProfileCreatedFrom(DateTime date)
    {
        _date = date;
    }

    public override Expression<Func<UserProfile, bool>> Build()
    {
        return a => a.CreatedOn >= _date;
    }
}