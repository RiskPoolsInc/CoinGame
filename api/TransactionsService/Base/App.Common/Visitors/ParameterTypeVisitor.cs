using System.Linq.Expressions;

namespace App.Common.Visitors;

internal class ParameterTypeVisitor<TFrom, TTo> : ExpressionVisitor {
    private readonly Expression<Func<TTo, TFrom>> _selector;

    public ParameterTypeVisitor(Expression<Func<TTo, TFrom>> selector) {
        _selector = selector;
    }

    protected override Expression VisitParameter(ParameterExpression node) {
        if (node.Type == typeof(TFrom))
            return Visit(_selector.Body);
        return base.VisitParameter(node);
    }
}