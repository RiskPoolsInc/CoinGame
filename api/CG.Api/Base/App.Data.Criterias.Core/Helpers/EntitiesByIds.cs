using System.Linq.Expressions;

using App.Interfaces.Data.Entities;

namespace App.Data.Criterias.Core.Helpers; 

public class EntitiesByIds<T> : ACriteria<T> where T : class, IEntity {
    private readonly Guid[] _ids;
    private readonly bool _include;

    public EntitiesByIds(Guid[] ids, bool include = true) {
        _ids = ids;
        _include = include;
    }

    public override Expression<Func<T, bool>> Build() {
        if (_ids.Length == 0)
            return a => _include ? a.Id == _ids[0] : a.Id != _ids[0];

        return a => _include ? _ids.Contains(a.Id) : !_ids.Contains(a.Id);
    }
}