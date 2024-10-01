using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Core;

namespace App.Data.Criterias.Dictionaries;

public class DictionaryEntityByIds<TDictionary> : ACriteria<TDictionary> where TDictionary : DictionaryEntity
{
    private readonly bool _excludeMode;
    private readonly int[] _ids;

    public DictionaryEntityByIds(int[] ids, bool excludeMode)
    {
        _ids = ids;
        _excludeMode = excludeMode;
    }

    public override Expression<Func<TDictionary, bool>> Build()
    {
        if (_ids?.Length > 0)
        {
            if (_excludeMode)
                return a => !_ids.Contains(a.Id);
            return a => _ids.Contains(a.Id);
        }

        return a => _excludeMode;
    }
}