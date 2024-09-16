namespace App.Interfaces.Security;

public interface ICurrentRequestClient {
    bool IsAnonymous { get; }
    public Guid ProfileId { get; set; }
}