using System.Linq.Expressions;
using System.Reflection;
using App.Common.Helpers;

namespace App.Data.Criterias.Core.Helpers; 

public sealed class FilterEntitySelector<TEntity> : PagedCriteria<TEntity> where TEntity : class {
    public string FilterName { get; set; }
    public string SearchExpr { get; set; }
    public bool IncludeEmptyIfExists { get; set; }

    public override Expression<Func<TEntity, bool>> Build() {
        return null;
    }

    public Expression<Func<TEntity, string>> BuildSelector() {
        var filterProperty = GetFilterProperty();

        var exprValue = $"{(filterProperty.IsPrimitive ? "(object)" : "")}p.{filterProperty.PropertyName}";
        var expr = QueryableHelper.SelectorDynamic<TEntity, string>(exprValue);

        return expr;
    }

    public Expression<Func<object, string>> BuildProjection() {
        var filterProperty = GetFilterProperty();

        if (!filterProperty.IsPrimitive)
            return s => "s";
        return s => "new FilterEntityView { Id = (string)(object)s, DisplayName = (string)(object)s }";
    }

    public string BuildFilter() {
        return !IncludeEmptyIfExists ? "s => s != null" : "s => true";
    }

    public string BuildSearchExpr() {
        return SearchExpr != null ? $@"s.DisplayName.Contains(""{SearchExpr}"")" : "true";
    }

    private FilterProperty GetFilterProperty() {
        var propertyName = FilterName.FromCamelCaseToTitleCase();
        var prop = GetProperty(typeof(TEntity), propertyName);

        var isPrimitive =
            prop.PropertyType.IsPrimitive ||
            prop.PropertyType == typeof(string) ||
            typeof(Enum).IsAssignableFrom(prop.PropertyType);

        var filterProperty = new FilterProperty { PropertyName = propertyName, IsPrimitive = isPrimitive };

        return filterProperty;
    }

    public static PropertyInfo GetProperty(Type src, string propName) {
        while (true) {
            if (src == null)
                throw new ArgumentException("Value cannot be null.", nameof(src));

            if (propName == null)
                throw new ArgumentException("Value cannot be null.", nameof(propName));

            if (propName.Contains(".")) {
                var temp = propName.Split(new[] { '.' }, 2);
                src = GetProperty(src, temp[0]).PropertyType;
                propName = temp[1];
            }
            else {
                return src.GetProperty(propName);
            }
        }
    }
}

public class FilterProperty {
    public string PropertyName { get; set; }
    public bool IsPrimitive { get; set; }
}