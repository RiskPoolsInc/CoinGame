using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using App.Interfaces.Security.Issuer;

namespace App.Security.Issuer;

public abstract class JwtTokenParam : IJwtTokenParam {
    private readonly Guid _sid;
    private readonly string _sub;

    protected JwtTokenParam(Guid sid, string sub) {
        _sid = sid;
        _sub = sub;
        JTI = Guid.NewGuid();
        Audience = new List<string>();
    }

    public Guid JTI { get; }

    public string ClientSecret { get; set; }
    public IList<string> Audience { get; set; }

    public Claim[] GetClaims() {
        var list = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, _sub),
            new(JwtRegisteredClaimNames.Jti, JTI.ToString("N")),
            new(JwtRegisteredClaimNames.Sid, _sid.ToString("N"))
        };

        foreach (var aud in Audience)
            list.Add(new Claim(JwtRegisteredClaimNames.Aud, aud));
        CreateClaims(list);
        return list.ToArray();
    }

    public abstract void CreateClaims(IList<Claim> claims);
}