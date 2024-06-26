using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.UserProfiles;

namespace App.Data.Entities.Attachments; 

public abstract class Attachment : AuditableEntity {
    public Guid? CreatedByUserId { get; set; }
    public virtual UserProfile?  CreatedByUser { get; set; }
    public int ObjectTypeId { get; set; }
    public Guid ObjectId { get; set; }
    public string? FileName { get; set; }
    public string? OriginalFileName { get; set; }
    public string? Description { get; set; }

    public byte[]? FileBody { get; set; }
    public int TypeId { get; set; }
    public virtual AttachmentType Type { get; set; }
    public virtual ObjectType ObjectType { get; set; }
}