using TS.Configuration;
using TS.Domain.Base.Interfaces;

namespace TS.Domain.Base
{
    public abstract class PagedCriterion<TResult> : QueryCriterionBase, IPagedCriterion<TResult>
    {
        public string Sort { get; set; }
        public int Direction { get; set; }

        public int? Page { get; set; }
        public int? Skip { get; set; }
        public int? Size { get; set; }
        public bool? GetAll { get; set; }
        public bool? WithDeleted { get; set; }
    }
}