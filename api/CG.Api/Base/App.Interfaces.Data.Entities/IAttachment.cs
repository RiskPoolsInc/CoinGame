using System;

namespace App.Interfaces.Data.Entities {

public interface IAttachment<T>
{
    public T Id { get; set; }
    public int EntityType { get; set; }
}

public interface IAttachment : IAttachment<Guid>
{
}
}