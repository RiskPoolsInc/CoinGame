using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.UserProfiles;

namespace App.Data.Entities.Notifications; 

public class Notification : AuditableEntity {
    public string Title { get; set; }
    public Guid ObjectId { get; set; }
    public string JsonObjectView { get; set; }

    public int TypeId { get; set; }
    public virtual AuditEventType Type { get; set; }

    public Guid? ParentId { get; set; }

    public Guid CreatedById { get; set; }
    public virtual UserProfile CreatedBy { get; set; }

    public Guid RecipientId { get; set; }
    public virtual UserProfile Recipient { get; set; }
}