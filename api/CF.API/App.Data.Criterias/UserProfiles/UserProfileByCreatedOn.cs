using App.Data.Entities.UserProfiles;

namespace App.Data.Criterias.UserProfiles;

public class UserProfileByCreatedOn : ACriteriaPeriod<UserProfile>
{
    public UserProfileByCreatedOn(DateTime? from = null, DateTime? to = null) : base(from, to)
    {
    }
}