using System.Linq.Expressions;

namespace App.Data.Criterias.Core;

public class ExpressionCriteria<TEntity> : ACriteria<TEntity> where TEntity : class {
    private readonly Expression<Func<TEntity, bool>> _expression;

    public ExpressionCriteria(Expression<Func<TEntity, bool>> expression) {
        _expression = expression;
    }

    public override Expression<Func<TEntity, bool>> Build() {
        return _expression;
    }
}