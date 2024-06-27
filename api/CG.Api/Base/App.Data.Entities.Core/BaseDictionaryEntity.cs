using App.Interfaces.Data.Entities;

namespace App.Data.Entities.Core;

public abstract class BaseDictionaryEntity : BaseEntity<int>, IBaseDictionaryEntity<int> {
    public string Name { get; set; }
    public string Code { get; set; }
}