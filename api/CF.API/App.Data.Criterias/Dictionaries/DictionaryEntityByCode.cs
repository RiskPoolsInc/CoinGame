using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Core;

namespace App.Data.Criterias.Dictionaries;

public class DictionaryEntityByCode<TDictionary> : AStartWithCriteria<TDictionary> where TDictionary : DictionaryEntity
{
    private readonly string _code;

    public DictionaryEntityByCode(string code) : base(code)
    {
        _code = code.ToLower();
    }

    protected override Expression<Func<TDictionary, string>> Property => a => a.Code;
}