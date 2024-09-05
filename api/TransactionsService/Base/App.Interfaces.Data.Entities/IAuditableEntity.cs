using System;

namespace App.Interfaces.Data.Entities;

public interface IAuditableEntity<TKey> : IEntity<TKey> {
    DateTime CreatedOn { get; set; }
    DateTime UpdatedOn { get; set; }
}

public interface IAuditableEntity : IAuditableEntity<Guid>, IEntity {
}