using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace App.Security.Issuer;

public class JwtUserTokenParam : JwtTokenParam {
    public JwtUserTokenParam(Guid userId, string email) : base(userId, email) {
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int TypeId { get; set; }

    public override void CreateClaims(IList<Claim> claims) {
        claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, FirstName));
        claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, LastName));
        claims.Add(new Claim(JwtCustomClaimNames.Type, TypeId.ToString()));
    }
}