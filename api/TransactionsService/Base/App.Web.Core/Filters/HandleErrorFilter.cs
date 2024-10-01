using System.Net;

using App.Core.Exceptions;
using App.Web.Core.Errors;
using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace App.Web.Core.Filters;

public class HandleErrorFilter : ExceptionFilterAttribute {
    private const string UnhandledException = "An unhandled exception has occurred while executing the request {0}";
    private readonly bool _isDevelopment;
    private readonly ILogger _logger;

    public HandleErrorFilter(ILogger<HandleErrorFilter> logger) {
#if DEBUG
        _isDevelopment = true;
#endif
        _logger = logger;
    }

    public override void OnException(ExceptionContext context) {
        base.OnException(context);

        switch (context.Exception) {
            case NotFoundException ne:
                SetNotFoundResult(context);
                break;
            case AccessDeniedException ae:
                SetForbiddenResult(context);
                break;
            case ValidationException ve:
                SetBadRequestResult(context, ve);
                break;
            case BadRequestException ex:
                SetBadRequestResult(context, ex);
                break;
            default:
                SetExceptionResult(context);
                break;
        }

        context.ExceptionHandled = true;
    }

    private void SetNotFoundResult(ExceptionContext context) {
        context.Result = new NotFoundObjectResult(new ErrorModel {
            Code = context.HttpContext.TraceIdentifier,
            Message = context.Exception.Message
        });
    }

    private void SetForbiddenResult(ExceptionContext context) {
        var result = new ObjectResult(new ErrorModel {
            Code = context.HttpContext.TraceIdentifier,
            Message = context.Exception.Message
        });
        result.StatusCode = (int)HttpStatusCode.Forbidden;
        context.Result = result;
    }

    private void SetBadRequestResult(ExceptionContext context, ValidationException exception) {
        context.Result = new BadRequestObjectResult(new ValidationResultModel(exception));
    }

    private void SetBadRequestResult(ExceptionContext context, BadRequestException exception) {
        context.Result = new BadRequestObjectResult(new ValidationResultModel(exception));
    }

    private void SetExceptionResult(ExceptionContext context) {
        _logger.LogError(context.Exception, string.Format(UnhandledException, context.HttpContext.TraceIdentifier));
        var result = new ObjectResult(new GenericErrorModel(context, _isDevelopment));
        result.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = result;
    }
}