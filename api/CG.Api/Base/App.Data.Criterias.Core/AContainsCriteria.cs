namespace App.Data.Criterias.Core;

public abstract class AContainsCriteria<TEntity> : ALikePatternCriteria<TEntity>
    where TEntity : class {
    public AContainsCriteria(string searchString) : base(searchString, Verb.Contains) {
    }
}