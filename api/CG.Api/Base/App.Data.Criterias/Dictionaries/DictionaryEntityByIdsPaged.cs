using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Core;

namespace App.Data.Criterias.Dictionaries;

public class DictionaryEntityByIdsPaged<TDictionary> : PagedCriteria<TDictionary>
    where TDictionary : DictionaryEntity
{
    private readonly bool _excludeMode;
    private readonly int[] _ids;

    public DictionaryEntityByIdsPaged(int[] ids, bool excludeMode = false)
    {
        _ids = ids;
        _excludeMode = excludeMode;
        Sort = "name";
    }

    public override Expression<Func<TDictionary, bool>> Build()
    {
        var criteria = True;
        if (string.IsNullOrWhiteSpace(Sort)) SetSortBy(a => a.Code);

        if (_ids?.Length > 0)
        {
            if (_excludeMode)
                return a => !_ids.Contains(a.Id);
            return a => _ids.Contains(a.Id);
        }

        return criteria.Build();
    }

    public override IQueryable<TDictionary> OrderBy(IQueryable<TDictionary> source)
    {
        switch (Sort)
        {
            case "name":
                return OrderByDirection(source, s => s.Name);
            case "code":
                return OrderByDirection(source, s => s.Code);
        }

        return base.OrderBy(source);
    }
}