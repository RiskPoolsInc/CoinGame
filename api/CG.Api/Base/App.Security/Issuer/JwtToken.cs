using App.Interfaces.Security.Issuer;

namespace App.Security.Issuer;

public class JwtToken : IJwtToken {
    public Guid TokenId { get; set; }
    public string AccessToken { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int Expires { get; set; }
}