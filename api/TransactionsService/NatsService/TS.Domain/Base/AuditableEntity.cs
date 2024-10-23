using System;
using SilentNotary.Common;

namespace TS.Domain.Base
{
    public abstract class AuditableEntity<TKey> : Entity<TKey>
    {
        public DateTime UpdatedOn { get; set; }
    }

    public abstract class AuditableEntity : AuditableEntity<Guid>
    {
    }
}