using System.ComponentModel;
using System.Reflection;

using App.Resources;

using MediatR;
using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace App.Core.Pipeline.Behaviors;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IRequestPreProcessor<TRequest>> _preProcessors;

    public LoggingBehaviour(IEnumerable<IRequestPreProcessor<TRequest>>    preProcessors,
                            ILogger<LoggingBehaviour<TRequest, TResponse>> logger = null) {
        _preProcessors = preProcessors;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
        if (_logger == null)
            return await HandleWithoutLoggingAsync(request, cancellationToken, next);

        _logger?.LogInformation(string.Format(LogMessages.Handling, typeof(TRequest).FullName));
        var myType = request.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

        foreach (var prop in props) {
            var propValue = prop.GetValue(request, null);
            _logger?.LogInformation("{Property} : {@Value}", prop.Name, propValue);
        }

        IList<DescriptionAttribute> attributes = myType.GetCustomAttributes<DescriptionAttribute>().ToList();

        foreach (var descriptionAttribute in attributes) {
            var descriptionValue = descriptionAttribute.Description;
            _logger?.LogInformation(string.Format(LogMessages.Description, descriptionValue));
        }

        try {
            foreach (var processor in _preProcessors)
                await processor.Process(request, cancellationToken).ConfigureAwait(false);

            var response = await next().ConfigureAwait(false);

            //Response
            _logger?.LogInformation(string.Format(LogMessages.Handled, typeof(TResponse).FullName));
            return response;
        }
        catch (Exception e) {
            _logger?.LogDebug(string.Format(LogMessages.HandlingWithError, typeof(TRequest).FullName, e));
            _logger?.LogError(string.Format(LogMessages.HandlingWithError, typeof(TRequest).FullName, e.Message));
            throw;
        }
    }

    public async Task<TResponse> HandleWithoutLoggingAsync(TRequest                          request, CancellationToken cancellationToken,
                                                           RequestHandlerDelegate<TResponse> next) {
        foreach (var processor in _preProcessors)
            await processor.Process(request, cancellationToken).ConfigureAwait(false);
        var response = await next().ConfigureAwait(false);

        //Response
        return response;
    }
}