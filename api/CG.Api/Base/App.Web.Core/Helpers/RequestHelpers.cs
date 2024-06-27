using Microsoft.AspNetCore.Http;

namespace App.Web.Core.Helpers;

public static class RequestHelper {
    public static string GetAbsolutePath(this HttpRequest request, string path, bool appendQueryString = false) {
        if (request == null)
            return string.Empty;

        var uriBuilder = new UriBuilder {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Path = path
        };

        if (request.Host.Port.HasValue)
            uriBuilder.Port = request.Host.Port.Value;

        if (appendQueryString && request.QueryString.HasValue)
            uriBuilder.Query = request.QueryString.Value;

        return uriBuilder.ToString();
    }
}