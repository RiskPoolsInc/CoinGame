using System.ComponentModel;
using System.Reflection;
using System.Text;

using Castle.DynamicProxy;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace App.Core.Logging;

public abstract class BaseMethodLogger<T> : MethodInvocationLogger<T> {
    private readonly ILogger<T> _logger;

    protected BaseMethodLogger(ILogger<T> logger = null) {
        _logger = logger;

        if (logger != null) {
            BeforeExecute += BeforeExecuteHandler;
            AfterExecute += AfterExecuteHandler;
            ErrorExecuting += ErrorExecutingHandler;
            TimeExecution += TimeExecutionHandler;
        }
    }

    private void TimeExecutionHandler(object obj, IInvocation invocation, long timeExecuting) {
        var executionTimeMessage = $"Execution Time: {timeExecuting} ms.";
        _logger.LogInformation(executionTimeMessage);
    }

    private void BeforeExecuteHandler(object obj, IInvocation invocation) {
        var methodNameView = GetMethodNameView(invocation);

        var descriptionAttributes = invocation.Method.CustomAttributes.OfType<DescriptionAttribute>().ToList();

        var callMessage = $"Executing: {methodNameView}";
        _logger.LogInformation(callMessage);

        if (descriptionAttributes.Count > 0) {
            var descriptionMessage = $"Description {methodNameView}: {descriptionAttributes.First().Description}";
            _logger.LogInformation(descriptionMessage);
        }

        if (invocation.Arguments?.Length > 0) {
            var argsMessage = string.Join(",", invocation.Arguments.Select(a => (a ?? "").ToString()));
            _logger.LogInformation($"With args: {argsMessage}");

            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(ArgumentsInfo(invocation.Arguments));
        }
    }

    private void AfterExecuteHandler(object obj, IInvocation invocation) {
        var methodNameView = GetMethodNameView(invocation);
        var returnedType = invocation.ReturnValue?.GetType().FullName ?? "none";
        var returnedTypeMessage = $"Executed {methodNameView} Returned type: {returnedType} ";
        _logger.LogInformation(returnedTypeMessage);
    }

    private void ErrorExecutingHandler(object obj, IInvocation invocation, Exception e) {
        var methodNameView = GetMethodNameView(invocation);
        var errorMessage = $"Exception {e} executing {methodNameView}";
        _logger.LogError(errorMessage);
    }

    internal string GetMethodNameView(IInvocation invocation) {
        var targetTypeName = $"Type: {invocation.TargetType.FullName}";
        var declaration = $"Declaration: {invocation.Method.DeclaringType}.{invocation.Method.Name}";
        return $"[{targetTypeName}] {declaration}";
    }

    internal virtual string ArgumentsInfo(params object[] arguments) {
        try {
            var argsJsonInfo = JsonConvert.SerializeObject(arguments);
            return argsJsonInfo;
        }
        catch (Exception e) {
            var argsDetails = new StringBuilder();

            for (var i = 0; i < arguments.Length; i++) {
                var invocationArgument = arguments[i];
                var myType = invocationArgument.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                var propsValueIsNull = props.Where(x => x.GetValue(invocationArgument, null) == null).ToList();
                var propsValueNotNull = props.Except(propsValueIsNull).ToList();
                argsDetails.AppendLine($"Arg[{i}]:{myType.FullName}");
                argsDetails.AppendLine("{");

                foreach (var prop in propsValueNotNull) {
                    var propValue = prop.GetValue(invocationArgument, null);
                    argsDetails.AppendLine($"   {prop.Name} : {propValue}");
                }

                if (propsValueIsNull.Count > 0) {
                    argsDetails.AppendLine("Value is NULL: [");
                    propsValueIsNull.ForEach(prop => argsDetails.AppendLine($"  {prop},"));
                    argsDetails.AppendLine("               ]");
                }

                argsDetails.AppendLine("}");
            }

            return argsDetails.ToString();
        }
    }
}