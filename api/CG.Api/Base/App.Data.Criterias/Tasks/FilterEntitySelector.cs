using App.Common.Helpers;
using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class FilterEntitySelector: PagedCriteria<TaskEntity> {
        public string FilterName { get; set; }
        public string SearchExpr { get; set; }
        public bool IncludeEmptyIfExists { get; set; }

        public override Expression<Func<TaskEntity, bool>> Build() {
            return null;
        }

        public Expression<Func<TaskEntity, string>> BuildSelector() {
            var exprValue = $"p.{FilterName.FromCamelCaseToTitleCase()}";
            var expr = QueryableHelper.SelectorDynamic<TaskEntity, string>(exprValue);
            return expr;
        }

        public string BuildFilter() {
            return !IncludeEmptyIfExists ? "s => s != null" : "s => true";
        }

        public string BuildSearchExpr() {
            return SearchExpr != null ? $@"s.DisplayName.Contains(""{SearchExpr}"")" : "true";
        }
    }
}
