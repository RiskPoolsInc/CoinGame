using System.Linq.Expressions;

namespace App.Data.Criterias.Core.Helpers; 

public static class ExpressionHelpers {
    public static string GetProperty(this Expression expression) {
        if (expression is UnaryExpression)
            expression = (expression as UnaryExpression).Operand;

        if (expression is MemberExpression) {
            var member = expression as MemberExpression;
            return member.Member.Name;
        }

        throw new NotSupportedException("MemberExpressions are supported only.");
    }

    public static bool PropertyInArray(this Expression expression, string[] array) {
        return array?.Select(a => a.ToLower()).Contains(expression.GetProperty().ToLower()) ?? false;
    }
}