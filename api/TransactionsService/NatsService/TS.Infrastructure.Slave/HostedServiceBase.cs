using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TS.Infrastructure.Slave
{
    // Original code taken from https://gist.github.com/davidfowl/a7dd5064d9dcf35b6eae1a7953d615e3
    public abstract class HostedServiceBase : IHostedService
    {
        private readonly bool _enabled;
        protected ILogger Logger { get; }

        private Task _executingTask;
        private CancellationTokenSource _cancellationTokenSource;

        protected HostedServiceBase(ILoggerFactory loggerFactory, bool enabled)
        {
            _enabled = enabled;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_enabled)
            {
                return Task.CompletedTask;
            }

            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = Task.Factory.StartNew(
                () => ExecuteAsync(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
            return _executingTask.IsCompleted
                ? _executingTask
                : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
                return;

            _cancellationTokenSource.Cancel();
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            cancellationToken.ThrowIfCancellationRequested();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}