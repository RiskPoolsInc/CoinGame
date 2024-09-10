using TS.Domain.Base.Interfaces;

namespace TS.Domain.Base
{
    public abstract class DictionaryEntity : Entity<int>, IDictionaryEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}