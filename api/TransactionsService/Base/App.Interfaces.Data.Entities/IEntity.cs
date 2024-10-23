using System;

namespace App.Interfaces.Data.Entities;

public interface IEntity<TKey> {
    TKey Id { get; set; }
}

public interface IEntity : IEntity<Guid>, IOrderedEntity {
}