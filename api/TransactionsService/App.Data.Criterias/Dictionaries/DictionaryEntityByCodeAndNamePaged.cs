using System.Linq.Expressions;

using App.Core.Requests;
using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Entities.Core;
using App.Interfaces.RequestsParams;

namespace App.Data.Criterias.Dictionaries;

public class DictionaryEntityByCodeAndNamePaged<TDictionary> : PagedCriteria<TDictionary>, IKeyWordFilter
    where TDictionary : DictionaryEntity
{
    public DictionaryEntityByCodeAndNamePaged(KeyWordFilter[] keywords)
    {
        KeyWords = keywords;
        Sort = "name";
    }

    public DictionaryFilter Filter { get; set; }
    public int[] ValuesOnTop { get; set; }
    public bool SortDisabled { get; set; }
    public bool ExcludeMode { get; set; }
    public bool TwoFactorFiltering { get; set; }
    public KeyWordFilter[] KeyWords { get; set; }

    public override Expression<Func<TDictionary, bool>> Build()
    {
        var criteria = True;

        if (TwoFactorFiltering)
        {
            criteria = criteria.And(new DictionaryEntityByIds<TDictionary>(ValuesOnTop, ExcludeMode));

            criteria = criteria.And(new DictionaryEntityByIds<TDictionary>(Filter?.SelectedIds,
                Filter?.ExcludeMode ?? false));
        }
        else
        {
            criteria = criteria
                .And(new DictionaryEntityByIds<TDictionary>(Filter?.SelectedIds, ExcludeMode));
        }

        if (string.IsNullOrWhiteSpace(Sort))
            SetSortBy(a => a.Code);

        if (!string.IsNullOrEmpty(KeyWords?.FirstOrDefault()?.KeyWord))
            criteria = criteria.And(new DictionaryByKeyWord<TDictionary>(KeyWords));

        return criteria.Build();
    }

    public override IQueryable<TDictionary> OrderBy(IQueryable<TDictionary> source)
    {
        if (SortDisabled)
            return source;

        return Sort?.ToLower() switch
        {
            "name" => OrderByDirection(source, s => s.Name),
            "code" => OrderByDirection(source, s => s.Code),
            _ => base.OrderBy(source)
        };
    }
}