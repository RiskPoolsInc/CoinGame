using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.UserProfiles;

namespace App.Data.Entities.UserProperties;

public class UserProperties : AuditableEntity {
    public int TypeId { get; set; }
    public virtual UserPropertyType Type { get; set; }

    public Guid UserProfileId { get; set; }
    public virtual UserProfile UserProfile { get; set; }

    public Guid? GuidValue { get; set; }
    public bool? BoolValue { get; set; }
    public string? TextValue { get; set; }
    public int? Int32Value { get; set; }
    public long? Int64Value { get; set; }
    public double? FloatValue { get; set; }
}