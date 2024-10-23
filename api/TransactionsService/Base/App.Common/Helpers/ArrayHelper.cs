namespace App.Common.Helpers;

public static class ArrayHelper {
    public static TResult[] SelectAs<TSource, TResult>(this TSource[] source) where TResult : class {
        return source.Select(a => a as TResult).ToArray();
    }
}