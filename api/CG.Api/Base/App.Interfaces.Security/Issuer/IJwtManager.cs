namespace App.Interfaces.Security.Issuer;

public interface IJwtManager {
    IJwtToken GetToken(IJwtTokenParam   parameters);
    Task<string> IssueTokenAsync(Guid   tokenId, CancellationToken cancellationToken);
    Task<Guid> DecryptTokenAsync(string token,   CancellationToken cancellationToken);
}