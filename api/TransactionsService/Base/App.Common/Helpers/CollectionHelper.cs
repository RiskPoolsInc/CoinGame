using System.Collections.Concurrent;

namespace App.Common.Helpers;

public static class CollectionHelper {
    public static void AddRange<T>(this ICollection<T> vessel, IEnumerable<T> source) {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        foreach (var item in source)
            vessel.Add(item);
    }

    public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> items) {
        items.ToList().ForEach(i => bag.Add(i));
    }

    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector) {
        var seenKeys = new HashSet<TKey>();

        foreach (var element in items)
            if (seenKeys.Add(keySelector(element)))
                yield return element;
    }

    public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size) {
        for (var i = 0; i < (float)array.Length / size; i++)
            yield return array.Skip(i * size).Take(size);
    }

    public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> original) {
        return original ?? Enumerable.Empty<T>();
    }
}