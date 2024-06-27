using App.Data.Criterias.Core;
using App.Interfaces.RequestsParams;

namespace App.Data.Criterias.Dictionaries;

public class DictionaryByKeyWord<TEntity> : KeyWordCriteria<TEntity> where TEntity : class
{
    public DictionaryByKeyWord(KeyWordFilter[] keywords)
    {
        Keywords = keywords;
    }
}