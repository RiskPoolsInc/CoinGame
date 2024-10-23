using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Core;

namespace App.Data.Criterias.Dictionaries;

internal class DictionaryEntityByName<TDictionary> : AContainsCriteria<TDictionary>
    where TDictionary : DictionaryEntity
{
    public DictionaryEntityByName(string name) : base(name)
    {
    }

    protected override Expression<Func<TDictionary, string>> Property => a => a.Name;
}