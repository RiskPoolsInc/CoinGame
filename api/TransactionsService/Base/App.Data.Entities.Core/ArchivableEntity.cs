using App.Interfaces.Data.Entities;

namespace App.Data.Entities.Core;

public abstract class ArchivableEntity<TKey> : AuditableEntity<TKey>, IArchivableEntity<TKey> {
    public DateTime? DateDeleted { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class ArchivableEntity : ArchivableEntity<Guid>, IArchivableEntity {
}