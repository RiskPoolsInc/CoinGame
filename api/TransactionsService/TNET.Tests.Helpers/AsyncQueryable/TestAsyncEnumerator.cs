using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TNET.Tests.Helpers.AsyncQueryable {
	public class TestAsyncEnumerator<T> : IAsyncEnumerator<T> {
		private readonly IEnumerator<T> _enumerator;

		public TestAsyncEnumerator(IEnumerator<T> enumerator) {
			_enumerator = enumerator ?? throw new ArgumentNullException();
		}

		public T Current => _enumerator.Current;

		public ValueTask DisposeAsync() {
			_enumerator.Dispose();
			return new ValueTask(Task.CompletedTask);
		}

		public ValueTask<bool> MoveNextAsync() {
			return new ValueTask<bool>(_enumerator.MoveNext());
		}
	}
}
