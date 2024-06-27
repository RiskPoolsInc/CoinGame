using System.Diagnostics;

using Castle.DynamicProxy;

namespace App.Core.Logging;

public abstract class MethodInvocationLogger<T> : IInterceptor {
    private Predicate<IInvocation> _filter;

    public Predicate<IInvocation> Filter {
        get => _filter;
        internal set { _filter = value ?? (m => true); }
    }

    public void Intercept(IInvocation invocation) {
        var timeWatch = Stopwatch.StartNew();
        OnBeforeExecute(invocation);

        try {
            invocation.Proceed();
            timeWatch.Stop();
            OnTimeExecution(invocation, timeWatch.ElapsedMilliseconds);
            OnAfterExecute(invocation);
        }
        catch (Exception e) {
            timeWatch.Stop();
            OnTimeExecution(invocation, timeWatch.ElapsedMilliseconds);
            OnErrorExecuting(invocation, e);
            throw;
        }
    }

    public event EventHandler<IInvocation> BeforeExecute = delegate { };
    public event EventHandler<IInvocation> AfterExecute = delegate { };
    public event Action<MethodInvocationLogger<T>, IInvocation, Exception> ErrorExecuting = delegate { };
    public event Action<MethodInvocationLogger<T>, IInvocation, long> TimeExecution = delegate { };

    private void OnBeforeExecute(IInvocation invocation) {
        if (_filter(invocation))
            BeforeExecute(this, invocation);
    }

    private void OnAfterExecute(IInvocation invocation) {
        if (_filter(invocation))
            AfterExecute(this, invocation);
    }

    private void OnErrorExecuting(IInvocation invocation, Exception exception) {
        if (_filter(invocation))
            ErrorExecuting(this, invocation, exception);
    }

    private void OnTimeExecution(IInvocation invocation, long totalMilliseconds) {
        if (_filter(invocation))
            TimeExecution(this, invocation, totalMilliseconds);
    }
}