using System.Security.Claims;

using CF.WebApi.Configuration;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CF.WebApi.Services; 

/// <summary>
///     Service for received user info from context.
/// </summary>
public class UserContextService : IUserContextService {
    private const string ClaimUserId = "UserId";

    private readonly HttpContext _httpContext;

    /// <inheritdoc />
    public UserContextService(IHttpContextAccessor httpContextAccessor) {
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <inheritdoc />
    public Guid GetUserId() {
        var claimWithUserId = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimUserId);

        if (claimWithUserId == null)
            throw new UnauthorizedAccessException("Current user unauthorized.");

        return new Guid(claimWithUserId.Value);
    }

    /// <inheritdoc />
    public string GetUserEmail() {
        var claimWithUserEmail = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);

        if (claimWithUserEmail == null)
            throw new UnauthorizedAccessException("Current user unauthorized.");

        return claimWithUserEmail.Value;
    }

    /// <inheritdoc />
    public string GetAccessToken() {
        return _httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token").GetAwaiter().GetResult();
    }
}