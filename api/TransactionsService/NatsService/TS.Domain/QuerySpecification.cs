using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TS.Configuration;

using Serialize.Linq.Serializers;
using SilentNotary.Specifications;

using TS.Domain.Enums;

namespace TS.Domain
{
    /// <summary>
    /// Data query specifications swapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuerySpecification<T> : Specification<T>, IQueryCriterionBase
    {
        public string ExpressionText;
        private Expression<Func<T, bool>> _predicate;
        private IEnumerable<Type> knownTypes = new[] { typeof(CoinType), typeof(TransactionFeeType) };

        // Constructor should (!!!) have internal modifier to restrict new specifications creation 
        // to only this project. This is because of intent of specification pattern to have all 
        // allowed criterions to query data in one place.
        public QuerySpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentException(nameof(predicate));
            var exprSerializer = new ExpressionSerializer (new JsonSerializer());
            exprSerializer.AddKnownTypes(knownTypes);
            ExpressionText = exprSerializer.SerializeText(_predicate);
        }

        public QuerySpecification()
        {
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate ?? (_predicate =
                       (Expression<Func<T, bool>>) new ExpressionSerializer(new JsonSerializer())
                           .DeserializeText(ExpressionText));
        }

        public string Key => QueryCriterionBase.DefaultKey;
    }
}