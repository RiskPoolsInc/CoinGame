namespace App.Interfaces.Security;

public interface ICurrentUser {
    bool IsAdmin { get; }
    bool IsCustomer { get; }
    bool IsExecutor { get; }
    bool IsAnonymous { get; }
    bool IsTaskManager { get; }
    Guid UserId { get; }
    Guid UserProfileId { get; }
}