namespace App.Interfaces.Security.Issuer;

public interface IJwtToken {
    Guid TokenId { get; }
    string AccessToken { get; }
    DateTime IssueDate { get; }
    DateTime ExpiryDate { get; }
    int Expires { get; }
}