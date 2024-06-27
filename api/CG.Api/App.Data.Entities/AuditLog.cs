using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.UserProfiles;

namespace App.Data.Entities; 

public class AuditLog : BaseEntity {
    public string IP { get; set; }

    public Guid? CreatedById { get; set; }
    public virtual UserProfile CreatedBy { get; set; }

    public int TypeId { get; set; }
    public virtual AuditEventType Type { get; set; }

    public Guid? UserId { get; set; }
    public virtual UserProfile User { get; set; }
}