namespace App.Data.Criterias.Core;

public abstract class AEndWithCriteria<TEntity> : ALikePatternCriteria<TEntity>
    where TEntity : class {
    public AEndWithCriteria(string searchString) : base(searchString, Verb.End) {
    }
}