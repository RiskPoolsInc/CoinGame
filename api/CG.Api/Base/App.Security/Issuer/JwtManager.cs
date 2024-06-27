using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using App.Core.Configurations;
using App.Interfaces.Security.Issuer;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace App.Security.Issuer;

public class JwtManager : IJwtManager {
    private readonly OAuthConfig _config;
    private readonly Rfc2898DeriveBytes _key;
    private readonly SymmetricAlgorithm _symAlgo;

    public JwtManager(OAuthConfig config) {
        _config = config;
        _key = new Rfc2898DeriveBytes(config.ClientSecret, Encoding.ASCII.GetBytes(config.ClientSecret));
        _symAlgo = new RijndaelManaged();
        _symAlgo.KeySize = 128;
        _symAlgo.Padding = PaddingMode.None;
        _symAlgo.Key = _key.GetBytes(_symAlgo.KeySize / 8);
        _symAlgo.IV = _key.GetBytes(_symAlgo.BlockSize / 8);
    }

    public IJwtToken GetToken(IJwtTokenParam parameters) {
        var now = DateTime.UtcNow;

        var symmetricKey = Convert.FromBase64String(parameters.ClientSecret);

        var tokenHandler = new JwtSecurityTokenHandler {
            TokenLifetimeInMinutes = _config.Ttl
        };

        var tokenDescriptor = new SecurityTokenDescriptor {
            Issuer = _config.Issuer,
            IssuedAt = now,
            NotBefore = now,
            Subject = GetIdentity(parameters),
            Expires = now.AddMinutes(_config.Ttl),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = new JwtToken {
            TokenId = parameters.JTI,
            IssueDate = now,
            ExpiryDate = tokenDescriptor.Expires.Value,
            Expires = _config.Ttl
        };
        var jwtSecurityToken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
        return token;
    }

    public async Task<string> IssueTokenAsync(Guid tokenId, CancellationToken cancellationToken) {
        var data = tokenId.ToByteArray();

        using (var stream = new MemoryStream()) {
            //TODO Refactoring - upgrade to 'using' declaration
            using (var crypto = new CryptoStream(stream, _symAlgo.CreateEncryptor(), CryptoStreamMode.Write)) {
                //TODO Refactoring - upgrade to 'await using' declaration
                await crypto.WriteAsync(data, 0, data.Length, cancellationToken);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }

    public async Task<Guid> DecryptTokenAsync(string token, CancellationToken cancellationToken) {
        var encrypted = Convert.FromBase64String(token);

        using (var stream = new MemoryStream()) {
            //TODO Refactoring - upgrade to 'using' declaration
            using (var crypto = new CryptoStream(stream, _symAlgo.CreateDecryptor(), CryptoStreamMode.Write)) {
                //TODO Refactoring - upgrade to 'await using' declaration
                await crypto.WriteAsync(encrypted, 0, encrypted.Length, cancellationToken);
                return new Guid(stream.ToArray());
            }
        }
    }

    private ClaimsIdentity GetIdentity(IJwtTokenParam parameters) {
        var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        identity.AddClaims(parameters.GetClaims());
        return identity;
    }
}