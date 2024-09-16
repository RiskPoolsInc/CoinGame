using App.Common.Helpers;
using App.Interfaces.Repositories.ServiceProfiles;
using App.Interfaces.Security;
using App.Security.Metadata;
using App.Security.Principals;

using Microsoft.AspNetCore.Http;

namespace App.Security.Context;

public class WebContextProvider : IContextProvider {
    private const string USER_AGENT_KEY = "User-Agent";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IServiceProfileRepository _serviceProfileRepository;
    private readonly Lazy<ICurrentRequestClient> _loader;
    private readonly Lazy<IRequestInfo> _metadata;

    public WebContextProvider(IHttpContextAccessor httpContextAccessor, IServiceProfileRepository serviceProfileRepository) {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _serviceProfileRepository = serviceProfileRepository;
        _loader = new Lazy<ICurrentRequestClient>(ParseContext, true);
        _metadata = new Lazy<IRequestInfo>(ParseMetadata, true);
    }

    public ICurrentRequestClient Context => _loader.Value;
    public IRequestInfo Request => _metadata.Value;
    public object GetProfile => _serviceProfileRepository.Get(Context.ProfileId.Value).Single();

    public string GetApiKey() {
        return _httpContextAccessor.HttpContext.GetValue<string>("x-api-key");
    }

    private ICurrentRequestClient ParseContext() {
        var apiKey = GetApiKey();

        if (!string.IsNullOrWhiteSpace(apiKey))
            return new WebPrincipal(apiKey, _serviceProfileRepository);
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