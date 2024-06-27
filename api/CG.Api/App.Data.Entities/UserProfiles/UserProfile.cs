using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;

namespace App.Data.Entities.UserProfiles; 

public class UserProfile : ArchivableEntity {
    public Guid UbikiriUserId { get; set; }
    public int TypeId { get; set; }
    public virtual UserType Type { get; set; }
}