using System;

namespace TS.Domain.Base
{
    public abstract class ArchivableEntity<TKey> : AuditableEntity<TKey>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    public abstract class ArchivableEntity : AuditableEntity
    {
    }
}