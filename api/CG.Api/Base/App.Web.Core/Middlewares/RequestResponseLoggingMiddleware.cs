using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace App.Web.Core.Middlewares;

public class RequestResponseLoggingMiddleware {
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger logger) {
        var request = await FormatRequest(context.Request);
        var originalBodyStream = context.Response.Body;
        logger.LogInformation($"RequestResponseLoggingMiddleware -> Request: {request}");

        using (var responseBody = new MemoryStream()) {
            context.Response.Body = responseBody;
            await _next(context);

            var response = await FormatResponse(context.Response);
            await responseBody.CopyToAsync(originalBodyStream);
            logger.LogInformation($"RequestResponseLoggingMiddleware -> Response: {response}");
        }
    }

    private async Task<string> FormatRequest(HttpRequest request) {
        var body = request.Body;
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body = body;

        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }

    private async Task<string> FormatResponse(HttpResponse response) {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();

        response.Body.Seek(0, SeekOrigin.Begin);

        return $"{response.StatusCode}: {text}";
    }
}