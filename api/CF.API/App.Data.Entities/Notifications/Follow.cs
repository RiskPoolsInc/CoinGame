using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.UserProfiles;

namespace App.Data.Entities.Notifications; 

public abstract class Follow : BaseEntity {
    public int TypeId { get; set; }
    public virtual FollowType Type { get; set; }

    public Guid FollowerId { get; set; }
    public virtual UserProfile Follower { get; set; }
}