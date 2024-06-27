using App.Data.Entities.Core;

namespace App.Core.Requests.Handlers.Helpers;

public static class DictionaryEntityExtensions {
    public static string GetNameForCodeEntity(this DictionaryEntity source) {
        if (!string.IsNullOrEmpty(source.Code))
            return source.Code + " - " + source.Name;
        return source.Name;
    }
}