using System.Linq;

using TS.Domain.Base.Interfaces;

namespace TS.Domain.Base.Extensions
{
    public static class PagerExtensions
    {
        public static IQueryable<TSource> PagerSkipTake<TSource>(this IQueryable<TSource> source, IPager iPager)
        {
            return iPager == null 
                ? source 
                : source.Skip((iPager.PageNumber.HasValue ? iPager.PageNumber - 1 : 1) * iPager.PageSize ?? int.MaxValue).Take(iPager.PageSize ?? int.MaxValue);
        }
    }
}