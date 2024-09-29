using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using TS.Tests.Helpers.AsyncQueryable;

namespace TS.Tests.Helpers {
    public static class MoqExtensions {
        public static IQueryable<TEntity> AsAsyncQueryable<TEntity>(this IEnumerable<TEntity> data) where TEntity : class {
            var query = data.AsQueryable();
            var mock = new Mock<IQueryable<TEntity>>();
            var enumerable = new TestAsyncEnumerable<TEntity>(query);

            mock.As<IAsyncEnumerable<TEntity>>().Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(enumerable.GetAsyncEnumerator());
            mock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(enumerable);
            mock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(query?.Expression);
            mock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(query?.ElementType);
            mock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(query?.GetEnumerator());
            return mock.Object;
        }
    }
}
