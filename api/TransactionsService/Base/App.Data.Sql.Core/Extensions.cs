namespace App.Data.Sql.Core;

public static class Extensions {
    public static T[,] To2DArray<T>(this IEnumerable<T[]> source) {
        return new[] { new T[source.Count(), source.ElementAt(0).Length] }
              .Select(x => new {
                   x,
                   y = source.Select((a, ia) => a.Select((b, ib) => x[ia, ib] = b).Count())
                             .Count()
               })
              .Select(a => a.x)
              .First();
    }
}