using System.Linq.Expressions;

namespace App.Common.Helpers;

public static class ReflectionHelper {
    public static string NameOf<T, TProp>(this T obj, Expression<Func<T, TProp>> expression) {
        var memberExp = expression.Body as MemberExpression;

        if (memberExp != null)
            return memberExp.Member.Name;

        var methodExp = expression.Body as MethodCallExpression;

        if (methodExp != null)
            return methodExp.Method.Name;

        throw new ArgumentException("'expression' should be a member expression or a method call expression.");
    }

    public static T GetValue<T>(this object obj, string property) {
        if (obj == null)
            return default;
        var path = property.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        if (path.Length == 0)
            return default;
        var prop = obj.GetType().GetProperty(path[0]);

        if (prop == null)
            return default;
        var res = prop.GetValue(obj, null);

        if (path.Length > 1)
            return GetValue<T>(res, property.Substring(property.IndexOf('.') + 1));
        return res == null ? default : (T)res;
    }

    public static void SetValue(this object obj, string property, object value) {
        if (obj == null || string.IsNullOrEmpty(property))
            return;
        var res = obj;

        while (property.IndexOf('.') > -1) {
            var path = property.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            res = res.GetType().GetProperty(path[0]).GetValue(res, null);
            property = property.Substring(property.IndexOf('.') + 1);

            if (res == null)
                return;
        }

        var prop = res.GetType().GetProperty(property);

        if (prop == null)
            return;
        prop.SetValue(res, value, null);
    }
}