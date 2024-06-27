using System;

namespace App.Interfaces.Data.Entities;

public interface IArchivableEntity<TKey> : IAuditableEntity<TKey> {
    bool IsDeleted { get; set; }
    DateTime? DateDeleted { get; set; }
}

public interface IArchivableEntity : IArchivableEntity<Guid>, IAuditableEntity {
}