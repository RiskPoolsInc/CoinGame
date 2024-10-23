using System;

using SilentNotary.Cqrs.Domain;

using TS.Domain.Base.Interfaces;

namespace TS.Domain.Base
{
    public abstract class Entity<TKey> : DomainEntityBase<TKey>, IEntity<TKey>
    {
        public DateTime CreatedOn { get; set; }
    }

    public abstract class Entity : Entity<Guid>
    {
    }
}