using App.Data.Criterias.Dictionaries;
using App.Data.Entities.Core;

namespace App.Core.Requests.Handlers.Helpers;

public static class DictionaryEntityFilterExtensions {
    public static void SetDefaultDirectionIfEmpty<TDictionary>(this DictionaryEntityByCodeAndNamePaged<TDictionary> filter)
        where TDictionary : DictionaryEntity {
        if (filter.Direction == 0)
            filter.Direction = 1;
    }

    public static void SetDefaultSortIfEmpty<TDictionary>(this DictionaryEntityByCodeAndNamePaged<TDictionary> filter, string defSort)
        where TDictionary : DictionaryEntity {
        if (string.IsNullOrEmpty(filter.Sort))
            filter.Sort = defSort;
    }

    public static void SetDefaultValuesIfEmpty<TDictionary>(this DictionaryEntityByCodeAndNamePaged<TDictionary> filter,
                                                            string                                               defSort = "name")
        where TDictionary : DictionaryEntity {
        filter.SetDefaultDirectionIfEmpty();
        filter.SetDefaultSortIfEmpty(defSort);
    }
}