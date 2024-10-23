using SilentNotary.Common.Query.Criterion.Abstract;

namespace TS.Configuration
{
    public interface IQueryCriterionBase : ICriterion
    {
        string Key { get; }
    }

    public abstract class QueryCriterionBase : IQueryCriterionBase
    {
        public const string DefaultKey = "DefaultQueueCriterion";
        public string Key => DefaultKey;
    }
}