using App.Data.Entities.UserProfiles;

namespace App.Data.Entities.Notifications; 

public class UserFollow : Follow {
    public Guid UserId { get; set; }
    public virtual UserProfile User { get; set; }
}