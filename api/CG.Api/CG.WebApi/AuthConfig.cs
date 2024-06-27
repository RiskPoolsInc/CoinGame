using System.Text;

using CF.WebApi.Configuration;
using CF.WebApi.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CF.WebApi; 

public static class AuthConfig {
    public static IServiceCollection AddAuthentication(this IServiceCollection services,
                                                       IConfiguration          configuration) {
        var identityOptions = configuration.GetSection("IdentitySettings").Get<IdentitySettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                     options.TokenValidationParameters = new TokenValidationParameters {
                         ValidateIssuer = true,
                         ValidIssuer = identityOptions.Url,
                         ValidateAudience = true,
                         ValidAudience = identityOptions.Url,
                         ValidateLifetime = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(identityOptions.SecretJwtKey))
                     };
                 });

        return services;
    }

    public static IServiceCollection AddHttpConfig(this IServiceCollection services) {
        return services.AddHttpContextAccessor()
                       .AddTransient(claims => claims.GetService<IHttpContextAccessor>().HttpContext.User)
                       .AddTransient<IUserContextService, UserContextService>()
                       .AddTransient<HttpClient>();
    }
}