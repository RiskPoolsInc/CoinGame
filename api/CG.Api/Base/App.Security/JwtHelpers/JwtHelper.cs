using System.Security.Claims;

namespace App.Security.JwtHelpers;

public static class JwtHelper {
    public static string GetValue(this ClaimsIdentity identity, string type) {
        var claim = identity.FindFirst(type);
        return claim?.Value ?? string.Empty;
    }
}