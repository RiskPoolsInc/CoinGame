using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Core;

namespace App.Data.Criterias.Dictionaries;

public class DictionariesByNames<TDictionary>
    : ACriteria<TDictionary> where TDictionary : DictionaryEntity
{
    private readonly bool _excludeMode;
    private readonly string[] _names;

    public DictionariesByNames(string[] names, bool excludeMode)
    {
        _names = names;
        _excludeMode = excludeMode;
    }

    public override Expression<Func<TDictionary, bool>> Build()
    {
        if (_names?.Length > 0)
        {
            if (_excludeMode)
                return a => !_names.Contains(a.Name);
            return a => _names.Contains(a.Name);
        }

        return a => _excludeMode;
    }
}