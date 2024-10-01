using System;

namespace TS.Domain
{
    public abstract class BaseView<TKey>
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseView : BaseView<Guid>
    {
    }
}