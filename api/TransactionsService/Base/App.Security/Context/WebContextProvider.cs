using System.Security.Claims;

using App.Interfaces.Security;
using App.Security.Metadata;
using App.Security.Principals;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace App.Security.Context;

public class WebContextProvider : IContextProvider {
    private const string USER_AGENT_KEY = "User-Agent";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Lazy<ICurrentRequestClient> _loader;
    private readonly Lazy<IRequestInfo> _metadata;

    public WebContextProvider(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _loader = new Lazy<ICurrentRequestClient>(ParseContext, true);
        _metadata = new Lazy<IRequestInfo>(ParseMetadata, true);
    }

    public ICurrentRequestClient Context => _loader.Value;
    public IRequestInfo Request => _metadata.Value;

    public string GetAccessToken() {
        return _httpContextAccessor.HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token")
                                   .GetAwaiter()
                                   .GetResult();
    }

    private ICurrentRequestClient ParseContext() {
        var identity = _httpContextAccessor.HttpContext.User.Identity;

        if (identity.IsAuthenticated)
            return new WebPrincipal(identity as ClaimsIdentity);
        return new AnonymousPrincipal();
    }

    private IRequestInfo ParseMetadata() {
        var context = _httpContextAccessor.HttpContext;

        return new WebRequestInfo {
            IP = context.Connection.RemoteIpAddress.ToString(),
            UserAgent = context.Request.Headers.ContainsKey(USER_AGENT_KEY)
                ? context.Request.Headers[USER_AGENT_KEY].ToString()
                : string.Empty
        };
    }
}