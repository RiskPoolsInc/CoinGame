using System;

namespace TS.Domain.Base.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime CreatedOn { get; set; }
    }
}