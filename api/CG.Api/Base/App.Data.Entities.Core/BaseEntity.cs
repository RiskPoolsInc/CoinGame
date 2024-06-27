using App.Interfaces.Data.Entities;

namespace App.Data.Entities.Core;

public abstract class BaseEntity<TKey> : IEntity<TKey> {
    public TKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<Guid>, IEntity {
    public DateTime CreatedOn { get; set; }
}