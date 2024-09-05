using System.Linq.Expressions;
using App.Common.Helpers;
using App.Data.Criterias.Core;
using App.Data.Entities.Core;

namespace App.Data.Criterias.Dictionaries;

public class DictionaryEntityByCodeAndName<TDictionary> : AComplexTextSearchCriteriaByCodesAndWords<TDictionary>
    where TDictionary : DictionaryEntity
{
    public DictionaryEntityByCodeAndName(string searchPhrase) : base(searchPhrase)
    {
    }

    public Expression<Func<TDictionary, bool>> BuildByCodes()
    {
        if (!Codes.Any())
            return True.Build();

        return Codes.Select(c => new DictionaryEntityByCode<TDictionary>(c).Build())
            .Aggregate((aggregated, current) => aggregated.Or(current));
    }

    public Expression<Func<TDictionary, bool>> BuildByNames()
    {
        if (!Words.Any())
            return True.Build();

        return Words.Select(c => new DictionaryEntityByName<TDictionary>(c).Build())
            .Aggregate((aggregated, current) => aggregated.And(current));
    }

    public override Expression<Func<TDictionary, bool>> Build()
    {
        return BuildByCodes().And(BuildByNames());
    }
}