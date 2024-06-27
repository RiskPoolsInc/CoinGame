using App.Interfaces.Data.Entities;

namespace App.Data.Entities.Core;

public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity<TKey> {
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}

public abstract class AuditableEntity : AuditableEntity<Guid>, IAuditableEntity {
}