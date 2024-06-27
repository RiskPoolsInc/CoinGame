namespace App.Data.Criterias.Core;

public abstract class AStartWithCriteria<TEntity> : ALikePatternCriteria<TEntity>
    where TEntity : class {
    public AStartWithCriteria(string searchString) : base(searchString, Verb.Start) {
    }
}