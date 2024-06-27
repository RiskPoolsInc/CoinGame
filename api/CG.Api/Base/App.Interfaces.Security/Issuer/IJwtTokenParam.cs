using System.Security.Claims;

namespace App.Interfaces.Security.Issuer;

public interface IJwtTokenParam {
    Guid JTI { get; }
    string ClientSecret { get; set; }
    IList<string> Audience { get; set; }
    Claim[] GetClaims();
    void CreateClaims(IList<Claim> claims);
}